
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.Number4;

public record Number4 : IRequest<Number4Dto>
{
    public int OperationId { get; set; }

}

public class Number4Handler : IRequestHandler<Number4, Number4Dto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public Number4Handler(IAppDbContext context, OperationEventHandler operationEvent)
    {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<Number4Dto> Handle(Number4 request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try {
                    var operation = _context.Operations
                    .Where(d => d.Id == request.OperationId)
                    .Include(o => o.Company)
                    .Include(o => o.Company.ContactPerson)
                    .Include(o => o.Containers)
                    .Select(o => new Operation {
                        Id = o.Id,
                        NameOnPermit = o.NameOnPermit,
                        Consignee = o.Consignee,
                        NotifyParty = o.NotifyParty,
                        BillNumber = o.BillNumber,
                        ShippingLine = o.ShippingLine,
                        GoodsDescription = o.GoodsDescription,
                        Quantity = o.Quantity,
                        GrossWeight = o.GrossWeight,
                        ATA = o.ATA,
                        FZIN = o.FZIN,
                        FZOUT = o.FZOUT,
                        DestinationType = o.DestinationType,
                        SourceDocument = o.SourceDocument,
                        ActualDateOfDeparture = o.ActualDateOfDeparture,
                        EstimatedTimeOfArrival = o.EstimatedTimeOfArrival,
                        VoyageNumber = o.VoyageNumber,
                        TypeOfMerchandise = o.TypeOfMerchandise,
                        OperationNumber = o.OperationNumber,
                        OpenedDate = o.OpenedDate,
                        Status = o.Status,
                        ECDDocument = o.ECDDocument,
                        ShippingAgentId = o.ShippingAgentId,
                        PortOfLoadingId = o.PortOfLoadingId,
                        CompanyId = o.CompanyId,
                        /////------------Additionals------
                        SNumber = o.SNumber, // operation
                        SDate = o.SDate, //operation
                        RecepientName = o.RecepientName,
                        VesselName = o.VesselName, // operation
                        ArrivalDate = o.ArrivalDate, // operation
                        ConnaissementNumber = o.ConnaissementNumber, // operation
                        CountryOfOrigin = o.CountryOfOrigin, // operation
                        REGTax = o.REGTax,//
                        BillOfLoadingNumber = o.BillOfLoadingNumber,
                        Company = new Company {
                            Name = o.Company.Name,
                            TinNumber = o.Company.TinNumber,
                            CodeNIF = o.Company.CodeNIF,
                            ContactPerson = new ContactPerson {
                                Name = o.Company.ContactPerson.Name
                            }
                        },
                        Containers = o.Containers == null ? null : o.Containers.Select(c => new Container {
                            ContianerNumber = c.ContianerNumber,
                            SealNumber = c.SealNumber
                        }).ToList()
                    }).FirstOrDefault();

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }
                    else if (!await _operationEvent.IsDocumentGenerated(request.OperationId,Enum.GetName(typeof(Documents) , Documents.GatePass)!)) {
                        throw new GhionException(CustomResponse.NotFound("Get pass should be generated!"));
                    }

                    var goods = await _context.Goods.Where(g => g.OperationId == request.OperationId).Include(g => g.Container).Select(g => new Good
                    {
                        Description = g.Description,
                        HSCode = g.HSCode,
                        Manufacturer = g.Manufacturer,
                        Weight = g.Weight,
                        Quantity = g.Quantity,
                        NumberOfPackages = g.NumberOfPackages,
                        Type = g.Type,
                        Location = g.Location,
                        ChasisNumber = g.ChasisNumber,
                        EngineNumber = g.EngineNumber,
                        ModelCode = g.ModelCode,
                        IsAssigned = g.IsAssigned,
                        ContainerId = g.ContainerId,
                        OperationId = g.OperationId,
                        TruckAssignmentId = g.TruckAssignmentId,
                        LocationPortId = g.LocationPortId
                    }).ToListAsync();

                    var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Name == ShippingAgentPaymentType.DeliveryOrder).FirstOrDefault();

                    if (payment == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("Payment Not found! either payment not made or payment is not filled on the system"));
                    }

                    await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Number4)!,
                        GeneratedDate = DateTime.Now,
                        IsApproved = false,
                        OperationId = request.OperationId
                    }, Enum.GetName(typeof(Status), Status.Number4Generated)!);
                   await transaction.CommitAsync();
                    return new Number4Dto {
                        company = operation.Company,
                        operation = operation,
                        containers = operation.Containers,
                        goods = goods,
                        doPayment = payment
                    };
                }catch (Exception) {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        });


    }
}