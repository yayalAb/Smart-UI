using System.Diagnostics.SymbolStore;

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Commands.CreateNumber4;

public record CreateNumber4Command : IRequest<CustomResponse>
{
    public int OperationId { get; set; }
    public int NameOnPermit {get; set;}
    public int ExitPortId {get; set;}
    public int DestinationPortId {get; set;}
    public string LoadType {get; set;}
    public ICollection<SelectedGood> Goods {get; set;}

}

public class CreateNumber4CommandHandler : IRequestHandler<CreateNumber4Command, CustomResponse> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public CreateNumber4CommandHandler(IAppDbContext context, OperationEventHandler operationEvent)
    {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<CustomResponse> Handle(CreateNumber4Command request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {

                    // if (!await _operationEvent.IsDocumentGenerated(request.OperationId, Enum.GetName(typeof(Documents), Documents.GatePass)!))
                    // {
                    //     throw new GhionException(CustomResponse.NotFound("Get pass should be generated!"));
                    // }

                    var operation = await _context.Operations.FindAsync(request.OperationId);

                    if (operation == null) {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }
                    
                    _context.GeneratedDocuments.Add(new GeneratedDocument {
                        LoadType = request.LoadType, 
                        DocumentType = Enum.GetName(typeof(Documents), Documents.Number4),
                        OperationId = request.OperationId,
                        ExitPortId = request.ExitPortId,
                        DestinationPortId = request.DestinationPortId,
                        ContactPersonId = request.NameOnPermit
                    });

                    await _context.SaveChangesAsync(cancellationToken);

                    var goods = await _context.Goods.Where(g => g.OperationId == request.OperationId).Include(g => g.Container).Select(g => new Good {
                        Description = g.Description,
                        HSCode = g.HSCode,
                        Weight = g.Weight,
                        Quantity = g.Quantity,
                        RemainingQuantity = g.RemainingQuantity,
                        Type = g.Type,
                        OperationId = g.OperationId,
                        LocationPortId = g.LocationPortId,
                        UnitPrice = g.UnitPrice,
                        Unit = g.Unit
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
                    // return new Number4Dto
                    // {
                    //     company = operation.Company,
                    //     operation = operation,
                    //     containers = operation.Containers,
                    //     goods = goods,
                    //     doPayment = payment
                    // };
                    return CustomResponse.Succeeded("successful");
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