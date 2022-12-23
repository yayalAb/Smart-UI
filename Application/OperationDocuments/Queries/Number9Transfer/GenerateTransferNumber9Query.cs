using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.ContainerModule;
using Application.OperationDocuments.Number9.N9Dtos;
using Application.OperationDocuments.Queries.Number9;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Common.PaymentTypes;
using Domain.Common.Units;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationDocuments.Queries.Number9Transfer;
public record GenerateTransferNumber9Query : IRequest<TransferNumber9Dto>
{
    public int? OperationId { get; set; }
    public int? NameOnPermitId { get; set; }
    public int? DestinationPortId { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }
    public bool isPrintOnly { get; set; }
    public int? GeneratedDocumentId { get; set; }
}
public class GenerateTransferNumber9QueryHandler : IRequestHandler<GenerateTransferNumber9Query, TransferNumber9Dto>
{
    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEventHandler;
    private readonly IMapper _mapper;
    private readonly DefaultCompanyService _defaultCompanyService;
    private readonly ILogger<GenerateTransferNumber9QueryHandler> _logger;
    private readonly GeneratedDocumentService _generatedDocumentService;

    public GenerateTransferNumber9QueryHandler(IAppDbContext context, OperationEventHandler operationEventHandler, IMapper mapper, DefaultCompanyService defaultCompanyService, ILogger<GenerateTransferNumber9QueryHandler> logger, GeneratedDocumentService generatedDocumentService)
    {
        _context = context;
        _operationEventHandler = operationEventHandler;
        _mapper = mapper;
        _defaultCompanyService = defaultCompanyService;
        _logger = logger;
        _generatedDocumentService = generatedDocumentService;
    }
    public async Task<TransferNumber9Dto> Handle(GenerateTransferNumber9Query request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    // save transferNO.9 doc for create
                    if (!request.isPrintOnly)
                    {
                        var createDocRequest = new CreateGeneratedDocDto
                        {
                            OperationId = (int)request.OperationId!,
                            NameOnPermitId = (int)request.NameOnPermitId!,
                            DestinationPortId = (int)request.DestinationPortId!,
                            documentType = Documents.TransferNumber9,
                            ContainerIds = request.ContainerIds,
                            GoodIds = request.GoodIds
                        };
                        request.GeneratedDocumentId = await _generatedDocumentService.CreateGeneratedDocumentRecord(createDocRequest, cancellationToken);

                        // generated document status
                        await _operationEventHandler.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                        {
                            GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.TransferNumber9)!,
                            GeneratedDate = DateTime.Now,
                            IsApproved = false,
                            OperationId = (int)request.OperationId
                        }, Enum.GetName(typeof(Status), Status.TransferNumber9Generated)!);
                    }


                    //fetch data
                    var doc = await _generatedDocumentService.fetchGeneratedDocument((int)request.GeneratedDocumentId!, cancellationToken);

                    var operation = new N9OperationDto
                    {
                        Id = doc.Operation.Id,
                        ShippingLine = doc.Operation.ShippingLine,
                        GoodsDescription = doc.Operation.GoodsDescription,
                        Quantity = doc.Operation.Quantity,
                        GrossWeight = doc.Operation.GrossWeight,
                        DestinationType = doc.Operation.DestinationType,
                        SourceDocument = doc.Operation.SourceDocument,
                        EstimatedTimeOfArrival = doc.Operation.EstimatedTimeOfArrival,
                        VoyageNumber = doc.Operation.VoyageNumber,
                        OperationNumber = doc.Operation.OperationNumber,
                        PortOfLoading = new N9PortOfLoadingDto
                        {
                            PortNumber = doc.Operation.PortOfLoading.PortNumber,
                            Country = doc.Operation.PortOfLoading.Country,
                            Region = doc.Operation.PortOfLoading.Region,
                            Vollume = doc.Operation.PortOfLoading.Vollume
                        },
                        CompanyId = doc.Operation.CompanyId,
                        SNumber = doc.Operation.SNumber,
                        SDate = doc.Operation.SDate,
                        VesselName = doc.Operation.VesselName,
                        ArrivalDate = doc.Operation.ArrivalDate,
                        CountryOfOrigin = doc.Operation.CountryOfOrigin,
                        REGTax = doc.Operation.REGTax,
                        Localization = doc.Operation.Localization,
                    };

                    var contactPerson = doc.ContactPerson;



                    var payment = _context.Payments.Where(c => c.OperationId == operation.Id && c.Name == ShippingAgentPaymentType.DeliveryOrder)
                            .Select(p => new N9PaymentDto
                            {
                                PaymentDate = p.PaymentDate,
                                DONumber = p.DONumber
                            }).FirstOrDefault();
                    var companySetting = await _defaultCompanyService.GetDefaultCompanyAsync();
                    await transaction.CommitAsync();
                    return new TransferNumber9Dto
                    {
                        defaultCompanyCodeNIF = companySetting.DefaultCompany.CodeNIF,
                        defaultCompanyName = companySetting.DefaultCompany.Name,
                        company = new N9CompanyDto { ContactPerson = _mapper.Map<N9NameOnPermitDto>(contactPerson) },
                        operation = operation,
                        doPayment = payment,
                        goods = doc.Goods,
                        containers = _mapper.Map<List<ContainerDto>>(doc.Containers),
                        TotalWeight = doc.LoadType == "Container" ? await _generatedDocumentService.ContainerCalculator("weight", doc.Containers) : await _generatedDocumentService.GoodCalculator("weight", doc.Goods),
                        TotalPrice = doc.LoadType == "Container" ? await _generatedDocumentService.ContainerCalculator("price", doc.Containers) : await _generatedDocumentService.GoodCalculator("price", doc.Goods),
                        TotalQuantity = doc.LoadType == "Container" ? doc.Containers.Count : doc.Goods.Count,
                        WeightUnit = WeightUnits.Default.name,
                        Currency = Currency.Default.name
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
}
