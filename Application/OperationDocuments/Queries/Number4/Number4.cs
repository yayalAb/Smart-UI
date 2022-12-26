
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.CompanyModule.Queries;
using Application.ContainerModule;
using Application.OperationDocuments.Number9.N9Dtos;
using Application.OperationDocuments.Queries.Number9Transfer;
using Application.OperationFollowupModule;
using Application.PortModule;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.PaymentTypes;
using Domain.Common.Units;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.Number4;

public record Number4 : IRequest<Number4Dto> {
    public int? OperationId { get; set; }
    public int? NameOnPermitId { get; set; }
    public int? DestinationPortId { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }
    public bool isPrintOnly { get; set; }
    public int? GeneratedDocumentId { get; set; }

}

public class Number4Handler : IRequestHandler<Number4, Number4Dto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;
    private readonly GeneratedDocumentService _generatedDocumentService;
    private readonly CurrencyConversionService _currencyConversionService;
    private readonly IMapper _mapper;

    public Number4Handler(
        IAppDbContext context, 
        OperationEventHandler operationEvent, 
        GeneratedDocumentService generatedDocumentService, 
        CurrencyConversionService currencyConversionService, 
        IMapper mapper
    ) {
        _context = context;
        _operationEvent = operationEvent;
        _generatedDocumentService = generatedDocumentService;
        _currencyConversionService = currencyConversionService;
        _mapper = mapper;
    }

    public async Task<Number4Dto> Handle(Number4 request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    // save No.4 doc for create
                    if (!request.isPrintOnly)
                    {
                        var createDocRequest = new CreateGeneratedDocDto
                        {
                            OperationId = (int)request.OperationId!,
                            NameOnPermitId = (int)request.NameOnPermitId!,
                            DestinationPortId = (int)request.DestinationPortId!,
                            documentType = Documents.Number4,
                            ContainerIds = request.ContainerIds,
                            GoodIds = request.GoodIds
                        };
                        request.GeneratedDocumentId = await _generatedDocumentService.CreateGeneratedDocumentRecord(createDocRequest, cancellationToken);
                        //save no4 operation status document
                        await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                        {
                            GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Number4)!,
                            GeneratedDate = DateTime.Now,
                            IsApproved = false,
                            OperationId = (int)request.OperationId
                        }, Enum.GetName(typeof(Status), Status.Number4Generated)!);
                    }
                    // fetch no.4 document 
                    var doc = await _generatedDocumentService.fetchGeneratedDocument((int)request.GeneratedDocumentId!, cancellationToken);

                    var operation = new Operation {
                        Id = doc.Operation.Id,
                        Consignee = doc.Operation.Consignee,
                        BillNumber = doc.Operation.BillNumber,
                        ShippingLine = doc.Operation.ShippingLine,
                        Quantity = doc.Operation.Quantity,

                        GrossWeight = doc.Operation.GrossWeight,
                        DestinationType = doc.Operation.DestinationType,
                        ActualDateOfDeparture = doc.Operation.ActualDateOfDeparture,
                        EstimatedTimeOfArrival = doc.Operation.EstimatedTimeOfArrival,
                        VoyageNumber = doc.Operation.VoyageNumber,
                        OperationNumber = doc.Operation.OperationNumber,
                        Localization = doc.Operation.Localization,
                        SNumber = doc.Operation.SNumber,
                        SDate = doc.Operation.SDate,
                        VesselName = doc.Operation.VesselName,
                        ArrivalDate = doc.Operation.ArrivalDate,
                        CountryOfOrigin = doc.Operation.CountryOfOrigin,
                        REGTax = doc.Operation.REGTax,
                        BillOfLoadingNumber = doc.Operation.BillOfLoadingNumber,

                        PortOfLoading = new Port {
                            PortNumber = doc.Operation.PortOfLoading.PortNumber,
                            Country = doc.Operation.PortOfLoading.Country,
                            Region = doc.Operation.PortOfLoading.Region,
                            Vollume = doc.Operation.PortOfLoading.Vollume
                        }
                        // _mapper.Map<N9PortOfLoadingDto>(doc.Operation.PortOfLoading) 

                    };

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }
                    // else if (!await _operationEvent.IsDocumentGenerated(request.OperationId, Enum.GetName(typeof(Documents), Documents.GatePass)!))
                    // {
                    //     throw new GhionException(CustomResponse.NotFound("Get pass should be generated!"));
                    // }
                    var payment = _context.Payments
                        .Where(c => c.OperationId == operation.Id && c.Name == ShippingAgentPaymentType.DeliveryOrder)
                        .ProjectTo<N9PaymentDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefault();

                    var data = new Number4Dto {
                        destinationPort = _mapper.Map<PortDto>(doc.DestinationPort),
                        company = null,//TODO: 
                        nameOnPermit = _mapper.Map<ContactPersonDto>(doc.ContactPerson),
                        operation = operation,
                        containers = doc.Containers.Count > 0 ? _mapper.Map<ICollection<ContainerDto>>(doc.Containers) : null,
                        goods = doc.Goods.Count > 0 ? doc.Goods : null,
                        doPayment = payment,
                        TotalWeight = doc.LoadType == "Container" ? await _generatedDocumentService.ContainerCalculator("weight", doc.Containers) : await _generatedDocumentService.GoodCalculator("weight", doc.Goods),
                        TotalPrice = doc.LoadType == "Container" ? await _generatedDocumentService.ContainerCalculator("price", doc.Containers) : await _generatedDocumentService.GoodCalculator("price", doc.Goods),
                        TotalQuantity = doc.LoadType == "Container" ? doc.Containers.Count : doc.Goods.Count,
                        WeightUnit = WeightUnits.Default.name,
                        Currency = Currency.Default.name
                    };
                    await transaction.CommitAsync();
                    return data;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        });


    }

}