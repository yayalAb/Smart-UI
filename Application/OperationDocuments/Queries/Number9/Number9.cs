using System.Linq;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;
using Application.OperationDocuments.Number9.N9Dtos;
using AutoMapper;
using Application.Common;

namespace Application.OperationDocuments.Queries.Number9;

public record Number9 : IRequest<Number9Dto>
{
    public int OperationId { get; init; }
    public string Type { get; init; }
}

public class Number9Handler : IRequestHandler<Number9, Number9Dto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;
    private readonly DefaultCompanyService _defaultCompanyService;


    public Number9Handler(IAppDbContext context, OperationEventHandler operationEvent , DefaultCompanyService defaultCompanyService , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
        _defaultCompanyService = defaultCompanyService;
    }

    public async Task<Number9Dto> Handle(Number9 request, CancellationToken cancellationToken)
    {

        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    var operation = _context.Operations.Where(d => d.Id == request.OperationId)
                                .Include(o => o.Company)
                                .Include(o => o.Goods)
                                .Include(o => o.PortOfLoading)
                                .Include(o => o.Company.ContactPerson)
                                .Select(o => new N9OperationDto {
                                    Id = o.Id,
                                    // NameOnPermit = o.NameOnPermit,
                                    // Consignee = o.Consignee,
                                    // NotifyParty = o.NotifyParty,
                                    // BillNumber = o.BillNumber,
                                    ShippingLine = o.ShippingLine,
                                    GoodsDescription = o.GoodsDescription,
                                    Quantity = o.Quantity,
                                    GrossWeight = o.GrossWeight,
                                    // ATA = o.ATA,
                                    // FZIN = o.FZIN,
                                    // FZOUT = o.FZOUT,
                                    DestinationType = o.DestinationType,
                                    SourceDocument = o.SourceDocument,
                                    // ActualDateOfDeparture = o.ActualDateOfDeparture,
                                    EstimatedTimeOfArrival = o.EstimatedTimeOfArrival,
                                    VoyageNumber = o.VoyageNumber,
                                    TypeOfMerchandise = o.TypeOfMerchandise,
                                    OperationNumber = o.OperationNumber,
                                    // OpenedDate = o.OpenedDate,
                                    // Status = o.Status,
                                    // ECDDocument = o.ECDDocument,
                                    // ShippingAgentId = o.ShippingAgentId,
                                    PortOfLoading = new N9PortOfLoadingDto {
                                        PortNumber = o.PortOfLoading.PortNumber,
                                        Country = o.PortOfLoading.Country,
                                        Region = o.PortOfLoading.Region,
                                        Vollume = o.PortOfLoading.Vollume
                                    },
                                    CompanyId = o.CompanyId,
                                    /////------------Additionals------
                                    SNumber = o.SNumber, // operation
                                    SDate = o.SDate, //operation
                                    // RecepientName = o.RecepientName,
                                    VesselName = o.VesselName, // operation
                                    ArrivalDate = o.ArrivalDate, // operation
                                    // ConnaissementNumber = o.ConnaissementNumber, // operation
                                    CountryOfOrigin = o.CountryOfOrigin, // operation
                                    REGTax = o.REGTax,//
                                    // BillOfLoadingNumber = o.BillOfLoadingNumber,
                                    Company = new N9CompanyDto {
                                        Name = o.Company.Name,
                                        TinNumber = o.Company.TinNumber,
                                        CodeNIF = o.Company.CodeNIF,
                                        ContactPersonId = o.Company.ContactPersonId,
                                        ContactPerson = _mapper.Map<N9NameOnPermitDto>(o.Company.ContactPerson),
                                    },
                                    Goods = (o.Goods != null) ? o.Goods.Select(g => new N9GoodDto
                                    {
                                        Description = g.Description,
                                        HSCode = g.HSCode,
                                        Manufacturer = g.Manufacturer,
                                        Weight = g.Weight,
                                        Quantity = g.Quantity,
                                        NumberOfPackages = g.NumberOfPackages,
                                        ChasisNumber = g.ChasisNumber,
                                        EngineNumber = g.EngineNumber,
                                        ModelCode = g.ModelCode,
                                        Unit = g.Unit,
                                        UnitPrice = g.UnitPrice,
                                        CBM = g.CBM,
                                        // Container = g.Container == null
                                        //                 ? null
                                        //                 : new Container
                                        //                 {
                                        //                     ContianerNumber = g.Container.ContianerNumber,
                                        //                     SealNumber = g.Container.SealNumber,
                                        //                     Location = g.Container.Location,
                                        //                     Size = g.Container.Size,
                                        //                     LocationPortId = g.Container.LocationPortId,
                                        //                     IsAssigned = g.Container.IsAssigned,
                                        //                     OperationId = g.Container.OperationId,
                                        //                 }
                                    }).ToList() : null,
                                    // Containers = (o.Containers == null) ? new List<Container>() : o.Containers.Select(c => new Container {
                                    //     ContianerNumber = c.ContianerNumber,
                                    //     SealNumber = c.SealNumber,
                                    //     Location = c.Location,
                                    //     Size = c.Size,
                                    //     LocationPortId = c.LocationPortId,
                                    //     IsAssigned = c.IsAssigned,
                                    //     OperationId = c.OperationId,
                                    //     TruckAssignmentId = c.TruckAssignmentId
                                    // }).ToList()
                                }).First();

                    if (operation == null) {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }

                    var goods = operation.Goods;
                    var company = operation.Company;
                    IEnumerable<float> size = new List<float>();
                    // size = from container in operation.Containers select container.Size;
                    operation.Company = null;
                    operation.Goods = null;
                    // operation.Containers = new List<Container>();

                    var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Name == ShippingAgentPaymentType.DeliveryOrder).Select(p => new N9PaymentDto {
                        // Name = p.Name,
                        // Type = p.Type,
                        PaymentDate = p.PaymentDate,
                        // PaymentMethod = p.PaymentMethod,
                        // BankCode = p.BankCode,
                        // Amount = p.Amount,
                        // Currency = p.Currency,
                        // Description = p.Description,
                        // OperationId = p.OperationId,
                        // ShippingAgentId = p.ShippingAgentId,
                        DONumber = p.DONumber
                    }).FirstOrDefault();

                    var companySetting = await _defaultCompanyService.GetDefaultCompanyAsync();


                    if (payment == null) {
                        throw new GhionException(CustomResponse.NotFound(" Delivery Order Payment for the operation not found!"));
                    }

                    await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = ((request.Type.ToLower() == "import") ? Enum.GetName(typeof(Documents), Documents.ImportNumber9) : Enum.GetName(typeof(Documents), Documents.TransferNumber9))!,
                        GeneratedDate = DateTime.Now,
                        IsApproved = false,
                        OperationId = request.OperationId
                    }, ((request.Type.ToLower() == "import") ? Enum.GetName(typeof(Status), Status.ImportNumber9Generated) : Enum.GetName(typeof(Status), Status.Closed))!);
                    await transaction.CommitAsync();
                    return new Number9Dto
                    {
                        defaultCompanyCodeNIF = companySetting.DefaultCompany.CodeNIF,
                        defaultCompanyName = companySetting.DefaultCompany.Name,
                        company = company,
                        operation = operation,
                        goods = goods,
                        doPayment = payment,
                        // containerSize = size
            
                    };

                }
                catch (Exception)
                {
                    await transaction.CommitAsync();
                    throw;
                }
            }
        });

    }
}