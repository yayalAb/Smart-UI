using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.OperationDocuments.Number9.N9Dtos;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Common.PaymentTypes;
using Domain.Common.Units;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.Number9;

public record Number9 : IRequest<Number9Dto>
{
    public int OperationId { get; init; }
    public string Type { get; init; }
    public int ContactPersonId { get; init; }
}

public class Number9Handler : IRequestHandler<Number9, Number9Dto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;
    private readonly DefaultCompanyService _defaultCompanyService;
    private readonly GeneratedDocumentService _generatedDocumentService;

    public Number9Handler(
        IAppDbContext context,
        OperationEventHandler operationEvent,
        DefaultCompanyService defaultCompanyService,
        GeneratedDocumentService generatedDocumentService,
        IMapper mapper
    )
    {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
        _defaultCompanyService = defaultCompanyService;
        _generatedDocumentService = generatedDocumentService;
    }

    public async Task<Number9Dto> Handle(Number9 request, CancellationToken cancellationToken)
    {

        var change = false;

        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {

            using (var transaction = _context.database.BeginTransaction())
            {

                try
                {
                    var operation = _context.Operations.Where(d => d.Id == request.OperationId)
                                .Include(o => o.Company)
                                .Include(o => o.Containers)
                                .Include(o => o.Goods)
                                .Include(o => o.PortOfLoading)
                                .Include(o => o.Company.ContactPeople)
                                .Select(o => new N9OperationDto
                                {
                                    Id = o.Id,
                                    ShippingLine = o.ShippingLine,
                                    GoodsDescription = o.GoodsDescription,
                                    Quantity = o.Quantity,
                                    BillNumber = o.BillNumber,
                                    GrossWeight = o.GrossWeight,
                                    DestinationType = o.DestinationType,
                                    SourceDocument = o.SourceDocument,
                                    EstimatedTimeOfArrival = o.EstimatedTimeOfArrival,
                                    VoyageNumber = o.VoyageNumber,
                                    OperationNumber = o.OperationNumber,
                                    PortOfLoading = new N9PortOfLoadingDto
                                    {
                                        PortNumber = o.PortOfLoading.PortNumber,
                                        Country = o.PortOfLoading.Country,
                                        Region = o.PortOfLoading.Region,
                                        Vollume = o.PortOfLoading.Vollume
                                    },
                                    CompanyId = o.CompanyId,
                                    /////------------Additionals------
                                    SNumber = o.SNumber, // operation
                                    SDate = o.SDate, //operation
                                    VesselName = o.VesselName, // operation
                                    ArrivalDate = o.ArrivalDate, // operation
                                    CountryOfOrigin = o.CountryOfOrigin, // operation
                                    REGTax = o.REGTax,//
                                    Company = new N9CompanyDto
                                    {
                                        Name = o.Company.Name,
                                        TinNumber = o.Company.TinNumber,
                                        CodeNIF = o.Company.CodeNIF
                                    },
                                    Goods = (o.Goods != null && request.Type.ToLower() == "unstaff") ? 
                                        o.Goods.Where(g => g.Type == request.Type).Select(g => new N9GoodDto {
                                            Description = g.Description,
                                            HSCode = g.HSCode,
                                            Weight = g.Weight,
                                            WeightUnit = g.WeightUnit,
                                            Type = g.Type,
                                            Quantity = g.Quantity,
                                            RemainingQuantity = g.RemainingQuantity,
                                            Unit = g.Unit,
                                            UnitPrice = g.UnitPrice,
                                            CBM = g.CBM
                                        }).ToList() : new List<N9GoodDto>(), 
                                    Containers = (o.Containers != null && request.Type.ToLower() == "container") ? 
                                        o.Containers.Select(c => new N9ContainerDto {
                                            Id = c.Id,
                                            ContianerNumber = c.ContianerNumber,
                                            GoodsDescription = c.GoodsDescription,
                                            SealNumber = c.SealNumber,
                                            Location = c.Location,
                                            Article = c.Article,
                                            Size = c.Size,
                                            GrossWeight = c.GrossWeight,
                                            WeightMeasurement = c.WeightMeasurement,
                                            Quantity = c.Quantity,
                                            TotalPrice = c.TotalPrice,
                                            Currency = c.Currency,
                                            LocationPortId = c.LocationPortId,
                                            OperationId = c.OperationId,
                                            GeneratedDocumentId = c.GeneratedDocumentId
                                        }).ToList() : new List<N9ContainerDto>()
                                }).First();

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }

                    var goods = operation.Goods;
                    var company = operation.Company;
                    var containers = operation.Containers;

                    //loading the selected name on permit //
                    var nameOnPermit = _mapper.Map<N9NameOnPermitDto>(await _context.ContactPeople.FindAsync(request.ContactPersonId));
                    company.ContactPerson = nameOnPermit;

                    operation.Company = null;
                    operation.Goods = null;
                    operation.Containers = null;

                    var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Name == ShippingAgentPaymentType.DeliveryOrder).Select(p => new N9PaymentDto
                    {
                        PaymentDate = p.PaymentDate,
                        DONumber = p.DONumber
                    }).FirstOrDefault();

                    var companySetting = await _defaultCompanyService.GetDefaultCompanyAsync();

                    if (payment == null)
                    {
                        throw new GhionException(CustomResponse.NotFound(" Delivery Order Payment for the operation not found!"));
                    }

                    change = await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.ImportNumber9),
                        GeneratedDate = DateTime.Now,
                        IsApproved = false,
                        OperationId = request.OperationId
                    }, Enum.GetName(typeof(Status), Status.ImportNumber9Generated));
                    await transaction.CommitAsync();

                    return new Number9Dto
                    {
                        defaultCompanyCodeNIF = companySetting.DefaultCompany.CodeNIF,
                        defaultCompanyName = companySetting.DefaultCompany.Name,
                        company = company,
                        operation = operation,
                        goods = goods,
                        container = containers,
                        doPayment = payment,
                        TotalWeight = request.Type.ToLower() == "container" ? await _generatedDocumentService.ContainerCalculator("weight", containers != null ? containers : new List<N9ContainerDto>()) : await _generatedDocumentService.GoodCalculator<N9GoodDto>("weight", (goods != null) ? goods : new List<N9GoodDto>()),
                        TotalPrice = request.Type.ToLower() == "container" ? await _generatedDocumentService.ContainerCalculator("price", containers != null ? containers : new List<N9ContainerDto>()) : await _generatedDocumentService.GoodCalculator<N9GoodDto>("price", goods != null ? goods : new List<N9GoodDto>()),
                        TotalQuantity = request.Type == "Container" ? containers!.Count : goods!.Count,
                        WeightUnit = WeightUnits.Default.name,
                        Currency = Currency.Default.name
                    };

                }
                catch (Exception)
                {
                    if (change)
                    {
                        await transaction.RollbackAsync();
                    }
                    throw;
                }

            }
        });

    }
}