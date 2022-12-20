
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.CompanyModule.Queries;
using Application.ContainerModule;
using Application.OperationDocuments.Number9Transfer;
using Application.OperationFollowupModule;
using Application.PortModule;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.Number4;

public record Number4 : IRequest<Number4Dto>
{
    public int OperationId { get; set; }
    public int NameOnPermitId { get; set; }
    public int DestinationPortId { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }

}

public class Number4Handler : IRequestHandler<Number4, Number4Dto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;
    private readonly GeneratedDocumentService _generatedDocumentService;
    private readonly IMapper _mapper;

    public Number4Handler(IAppDbContext context, OperationEventHandler operationEvent , GeneratedDocumentService generatedDocumentService , IMapper mapper )
    {
        _context = context;
        _operationEvent = operationEvent;
        _generatedDocumentService = generatedDocumentService;
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
                
                    // save No.4 doc
                    var createDocRequest = new CreateGeneratedDocDto
                    {
                        OperationId = request.OperationId,
                        NameOnPermitId = request.NameOnPermitId,
                        DestinationPortId = request.DestinationPortId,
                        documentType = Documents.TransferNumber9,
                        ContainerIds = request.ContainerIds,
                        GoodIds = request.GoodIds
                    };
                var createDocResult = await _generatedDocumentService.CreateGeneratedDocumentRecord(createDocRequest, cancellationToken);

                var operation = _context.Operations
                .Where(d => d.Id == request.OperationId)
                .Include(o => o.Company)
                .Include(o => o.Company.ContactPeople)
                .Include(o => o.Containers)
                .Include(o => o.PortOfLoading)
                .Select(o => new Operation
                {
                    Id = o.Id,
                    Consignee = o.Consignee,
                    BillNumber = o.BillNumber,
                    ShippingLine = o.ShippingLine,
                    Quantity = o.Quantity,
                    
                    GrossWeight = o.GrossWeight,
                    DestinationType = o.DestinationType,
                    ActualDateOfDeparture = o.ActualDateOfDeparture,
                    EstimatedTimeOfArrival = o.EstimatedTimeOfArrival,
                    VoyageNumber = o.VoyageNumber,
                    OperationNumber = o.OperationNumber,
                    Localization = o.Localization,
                    /////------------Additionals------
                    SNumber = o.SNumber, // operation
                    SDate = o.SDate, //operation
                    VesselName = o.VesselName, // operation
                    ArrivalDate = o.ArrivalDate, // operation
                    CountryOfOrigin = o.CountryOfOrigin, // operation
                    REGTax = o.REGTax,//
                    BillOfLoadingNumber = o.BillOfLoadingNumber,
                    
                    PortOfLoading = new Port
                    {
                        PortNumber = o.PortOfLoading.PortNumber,
                        Country = o.PortOfLoading.Country,
                        Region = o.PortOfLoading.Region,
                        Vollume = o.PortOfLoading.Vollume
                    },
                    Company = new Company
                    {
                        Name = o.Company.Name,
                        TinNumber = o.Company.TinNumber,
                        CodeNIF = o.Company.CodeNIF,
                    },
                }).FirstOrDefault();

                if (operation == null)
                {
                    throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                }
                // else if (!await _operationEvent.IsDocumentGenerated(request.OperationId, Enum.GetName(typeof(Documents), Documents.GatePass)!))
                // {
                //     throw new GhionException(CustomResponse.NotFound("Get pass should be generated!"));
                // }
                var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Name == ShippingAgentPaymentType.DeliveryOrder).FirstOrDefault();
                var contactPerson = await _context.ContactPeople
                        .Where(cp => cp.Id == request.NameOnPermitId)
                        .ProjectTo<ContactPersonDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                if (contactPerson == null)
                {
                    throw new GhionException(CustomResponse.NotFound($"contact person with id = {request.NameOnPermitId}"));
                }

                await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                {
                    GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Number4)!,
                    GeneratedDate = DateTime.Now,
                    IsApproved = false,
                    OperationId = request.OperationId
                }, Enum.GetName(typeof(Status), Status.Number4Generated)!);
                await transaction.CommitAsync();
                return new Number4Dto
                {
                    destinationPort = _mapper.Map<PortDto>(createDocResult.destinationPort),
                    company = operation.Company,
                    nameOnPermit = contactPerson,
                    operation = operation,
                    containers =_mapper.Map<ICollection<ContainerDto>>( createDocResult.containers),
                    goods = _mapper.Map<ICollection<DocGoodDto>>(createDocResult.goods),
                    doPayment = payment
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