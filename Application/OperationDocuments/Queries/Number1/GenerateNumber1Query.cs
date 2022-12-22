
using Application.Common;
using Application.Common.Service;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.OperationDocuments.Queries.Number9Transfer;
using Application.OperationFollowupModule;
using Application.PortModule;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.Number1;

public record GenerateNumber1Query : IRequest<Number1Dto>
{
    public int OperationId { get; init; }
    public int NameOnPermitId { get; set; }
    public int DestinationPortId { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }
    public bool isPrintOnly { get; set; }
    public int? GeneratedDocumentId { get; set; }
}

public class GenerateNumber1QueryHandler : IRequestHandler<GenerateNumber1Query, Number1Dto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;
    private readonly DefaultCompanyService _defaultCompanyService;
    private readonly GeneratedDocumentService _generatedDocumentService;

    public GenerateNumber1QueryHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent, DefaultCompanyService defaultCompanyService, GeneratedDocumentService generatedDocumentService)
    {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
        _defaultCompanyService = defaultCompanyService;
        _generatedDocumentService = generatedDocumentService;
    }

    public async Task<Number1Dto> Handle(GenerateNumber1Query request, CancellationToken cancellationToken)
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
                    // save No.1 doc for create
                    if (!request.isPrintOnly)
                    {
                        var createDocRequest = new CreateGeneratedDocDto
                        {
                            OperationId = request.OperationId,
                            NameOnPermitId = request.NameOnPermitId,
                            DestinationPortId = request.DestinationPortId,
                            documentType = Documents.TransferNumber9,
                            ContainerIds = request.ContainerIds,
                            GoodIds = request.GoodIds
                        };
                         request.GeneratedDocumentId = await _generatedDocumentService.CreateGeneratedDocumentRecord(createDocRequest, cancellationToken);

                    }

                    var date = DateTime.Now;
                    // fetch number1 data
                    var doc = await _generatedDocumentService.fetchGeneratedDocument((int)request.GeneratedDocumentId!, cancellationToken);

                    Number1Dto data = new Number1Dto
                    {
                        Date = doc.Operation.ArrivalDate,
                        BillOfLoadingNumber = doc.Operation.BillOfLoadingNumber,
                        PortOfLoadingCountry = doc.Operation.PortOfLoading.Country,
                        SNumber = doc.Operation.SNumber,
                        SDate = doc.Operation.SDate,
                        TotalNumberOfPackages = doc.Goods.Select(g => g.Quantity).Sum(),
                        RecepientName = doc.Operation.RecepientName,
                        VesselName = doc.Operation.VesselName,
                        ArrivalDate = doc.Operation.ArrivalDate,
                        VoyageNumber = doc.Operation.VoyageNumber,
                        CountryOfOrigin = doc.Operation.CountryOfOrigin,
                        REGTax = doc.Operation.REGTax,
                        Goods = doc.Goods,
                        DestinationLocation = _mapper.Map<PortDto>(doc.DestinationPort),
                        Containers = doc.Containers.Select(c => new No1ContainerDto
                        {
                            ContianerNumber = c.ContianerNumber,
                            SealNumber = c.SealNumber

                        }).ToList(),
                        SourceLocation = doc.Operation.Localization,
                    };
                    //fetch do payment data if paid 
                    var payment = _context.Payments
                           .Where(c => c.OperationId == doc.Operation.Id && c.Name == ShippingAgentPaymentType.DeliveryOrder)
                           .FirstOrDefault();
                    if (payment != null)
                    {
                        data.DONumber = payment.DONumber;
                        data.DODate = payment.PaymentDate;
                    }

                    // fetch ghion info
                    var settingData = await _defaultCompanyService.GetDefaultCompanyAsync();
                    data.DefaultCompanyName = settingData!.DefaultCompany.Name;
                    data.DefaultCompanyCodeNIF = settingData.DefaultCompany.CodeNIF;

                    // update operation status and generate doc
                    var statusName = Enum.GetName(typeof(Status), Status.Number1Generated);
                    await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Number1)!,
                        GeneratedDate = date,
                        IsApproved = false,
                        OperationId = request.OperationId
                    }, statusName!);
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