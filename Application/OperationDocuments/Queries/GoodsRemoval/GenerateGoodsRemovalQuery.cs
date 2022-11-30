using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
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
        if (!await _context.Operations.AnyAsync(o => o.Id == request.OperationId))
        {
            throw new GhionException(CustomResponse.NotFound("There is no Operation with the given Id!"));
        }
        List<TruckAssignmentDto> truckAssignments = new List<TruckAssignmentDto>();
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
        for (int i = 0; i < request.TruckAssignmentIds.Count; i++)
        {
            var ta = await _context.TruckAssignments.Where(ta => ta.Id == request.TruckAssignmentIds[i])
                        .Include(ta => ta.Truck)
                        .Include(ta => ta.Driver)
                        .Include(ta => ta.Containers)
                            .ThenInclude(c => c.Goods)
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
                            Quantity = getQuantity(ta.Containers, ta.Goods),
                            Weight = getWeight(ta.Containers, ta.Goods),
                            Description = getDescription(ta.Containers, ta.Goods)

                        }).FirstOrDefaultAsync();
            truckAssignments.Add(ta);
        }

        // update operation status and generate doc
        var date = DateTime.Now;
        var statusName = Enum.GetName(typeof(Status), Status.GoodsRemovalGenerated);
        await _operationEventHandler.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
        {
            GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.GoodsRemoval)!,
            GeneratedDate = date,
            IsApproved = false,
            OperationId = request.OperationId
        },
         statusName!
         );

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
    private static string? getQuantity(ICollection<Container> containers, ICollection<Good> goods)
    {

        if (containers != null)
        {
            List<string> quantitiyList = new List<string>();
            var sizes = containers.Select(c => c.Size);

            foreach (var grp in sizes.GroupBy(i => i))
            {

                quantitiyList.Add($"{grp.Count()}x{grp.Key}");
            }
            return string.Join(",", quantitiyList);
        }
        if (goods != null && goods.Count > 0)
        {
            return goods.Select(g => g.NumberOfPackages).Sum().ToString();
        }
        return null;
    }

    private static float? getWeight(ICollection<Container> containers, ICollection<Good> goods)
    {
        float weight = 0;
        if ((containers == null || containers.Count == 0) && (goods == null || goods.Count == 0))
        {
            return null;
        }
        if (containers != null && containers.Count > 0)
        {
            weight += containers.SelectMany(c => c.Goods).Select(g => g.Weight).Sum();
        }
        if (goods != null && goods.Count > 0)
        {
            weight += goods.Select(g => g.Weight).Sum();
        }
        return weight;
    }

    private static IEnumerable<string>? getDescription(ICollection<Container> containers, ICollection<Good> goods)
    {
        if (containers != null && containers.Count > 0)
        {
            return containers.SelectMany(c => c.Goods).Select(g => g.Description);
        }
        if (goods != null && goods.Count > 0)
        {
            return goods.Select(g => g.Description);
        }
        return null;
    }
}