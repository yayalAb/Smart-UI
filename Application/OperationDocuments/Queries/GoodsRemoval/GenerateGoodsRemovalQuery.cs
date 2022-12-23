using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.GoodsRemoval;
public record GenerateGoodsRemovalQuery : IRequest<GoodsRemovalDto>
{
    public int OperationId { get; init; }
    public List<int> TruckAssignmentIds { get; init; }
}
public class GenerateGoodsRemovalQueryHandler : IRequestHandler<GenerateGoodsRemovalQuery, GoodsRemovalDto>
{
    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEventHandler;

    public GenerateGoodsRemovalQueryHandler(IAppDbContext context, OperationEventHandler operationEventHandler)
    {
        _context = context;
        _operationEventHandler = operationEventHandler;
    }
    public async Task<GoodsRemovalDto> Handle(GenerateGoodsRemovalQuery request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    if (!await _context.Operations.AnyAsync(o => o.Id == request.OperationId))
                    {
                        throw new GhionException(CustomResponse.NotFound("There is no Operation with the given Id!"));
                    }
                    //checking preconditions before generating goods removal
                    if (!await _operationEventHandler.IsDocumentApproved(request.OperationId, Enum.GetName(typeof(Documents), Documents.T1)!))
                    {
                        throw new GhionException(CustomResponse.BadRequest($"T1 must be approved before generating goods removal document "));
                    }
                    // fetch data from db
                    var operationData = await _context.Operations.Where(o => o.Id == request.OperationId)
                                                .Include(o => o.PortOfLoading)
                                                .Include(o => o.Company)
                                                .Include(o => o.ShippingAgent)
                                                .Select(o => new
                                                {
                                                    ImporterName = o.Company.Name,
                                                    ShippingAgnetName = o.ShippingAgent == null
                                                                                        ? null
                                                                                        : o.ShippingAgent.FullName,
                                                    BillOfLoadingNumber = o.BillOfLoadingNumber,
                                                    LocationOfLoading = o.PortOfLoading == null
                                                                                        ? null
                                                                                        : o.PortOfLoading.Country,
                                                    VoyageNumber = o.VoyageNumber
                                                }!
                                                ).FirstOrDefaultAsync();    
                    var truckAssignments = await _context.TruckAssignments.Where(ta => request.TruckAssignmentIds.Contains(ta.Id))
                                        .Include(ta => ta.Truck)
                                        .Include(ta => ta.Driver)
                                        .Include(ta => ta.Containers)!
                                        .Include(ta => ta.Goods)
                                        .Select(ta => new TruckAssignmentDto
                                        {
                                            PlateNumber = ta.Truck == null
                                                                            ? null
                                                                            : ta.Truck.PlateNumber,
                                            DriverName = ta.Driver == null
                                                                            ? null
                                                                            : ta.Driver.Fullname,
                                            DriverPhone = ta.Driver == null
                                                                            ? null
                                                                            : ta.Driver.Address.Phone,
                                            ContainerNumbers = ta.Containers == null || ta.Containers.Count == 0
                                                                            ? null
                                                                            : ta.Containers.Select(c => c.ContianerNumber).ToList(),
                                            Quantity = ta.Containers != null?
                                                            string.Join(",", ta.Containers.GroupBy(c => c.Size).Select(grp => $"{grp.Count()}X{grp.Key}").ToList())
                                                        :ta.Goods != null ?
                                                            ta.Goods.Select(g => g.Quantity).Sum().ToString()
                                                        :null,

                                            // Quantity = getQuantity(ta.Containers, ta.Goods),
                                            Weight = getWeight(ta.Containers, ta.Goods),
                                            Description = getDescription(ta.Containers, ta.Goods)

                                        }).ToListAsync();
                    var difference = truckAssignments.Count - request.TruckAssignmentIds.Count;
                    if(difference != 0){
                        throw new GhionException(CustomResponse.NotFound($"{difference} truck assignments with the provided id is not found"));
                    }
                    // update operation status and generate doc
                    var date = DateTime.Now;
                    var statusName = Enum.GetName(typeof(Status), Status.GoodsRemovalGenerated);

                    // transaction is used for this method
                    await _operationEventHandler.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.GoodsRemoval)!,
                        GeneratedDate = date,
                        IsApproved = false,
                        OperationId = request.OperationId
                    },
                         statusName!
                         );
                    await transaction.CommitAsync();
                    return new GoodsRemovalDto
                    {
                        Date = date,
                        Declarant = null,
                        ImporterName = operationData?.ImporterName,
                        ShippingAgentName = operationData?.ShippingAgnetName,
                        BillOfloadingNumber = operationData?.BillOfLoadingNumber,
                        REFNumber = null,
                        ClearanceOffice = null,
                        FrontierOffice = null,
                        LocationOfLoading = operationData?.LocationOfLoading,
                        VoyageNumber = operationData?.VoyageNumber,
                        DeclarationNumber = null,
                        FinalDestination = null,
                        TruckAssignments = truckAssignments
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        });

    }
    // private static string? getQuantity(ICollection<Container> containers, ICollection<Good> goods)
    // {

    //     if (containers != null)
    //     {
    //         List<string> quantitiyList = new List<string>();
    //         var sizes = containers.Select(c => c.Size);
    //         foreach (var grp in sizes.GroupBy(i => i))
    //         {

    //             quantitiyList.Add($"{grp.Count()}x{grp.Key}");
    //         }
    //         return string.Join(",", quantitiyList);
    //     }
    //     if (goods != null && goods.Count > 0)
    //     {
    //         return goods.Select(g => g.Quantity).Sum().ToString();
    //     }
    //     return null;
    // }

    private static float? getWeight(ICollection<Container>? containers, ICollection<Good>? goods)
    {
        float weight = 0;
        if ((containers == null || containers.Count == 0) && (goods == null || goods.Count == 0))
        {
            return null;
        }
        if (containers != null && containers.Count > 0)
        {
           weight += containers.Select(c => AppdivConvertor.WeightConversion(c.WeightMeasurement,  c.GrossWeight)).Sum();
        }
        if (goods != null && goods.Count > 0)
        {
            weight += goods.Select(g => AppdivConvertor.WeightConversion(g.WeightUnit,  g.Weight)).Sum();
        }
        return weight;
    }

    private static IEnumerable<string>? getDescription(ICollection<Container> containers, ICollection<Good> goods)
    {
        if (containers != null && containers.Count > 0)
        {
            return containers.Select(c => c.GoodsDescription)!;
        }
        if (goods != null && goods.Count > 0)
        {
            return goods.Select(g => g.Description);
        }
        return null;
    }
}