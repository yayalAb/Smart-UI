
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

public record Number4 : IRequest<Number4Dto> {
    public int OperationId {get; set;}

}

public class Number4Handler : IRequestHandler<Number4, Number4Dto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public Number4Handler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }
    
    public async Task<Number4Dto> Handle(Number4 request, CancellationToken cancellationToken) {

        var operation = _context.Operations.Where(d => d.Id == request.OperationId).Include(o => o.Company).FirstOrDefault();
        
        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == request.OperationId).Include(g => g.Container).ToListAsync();

        var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Type == ShippingAgentPaymentType.DeliveryOrder).FirstOrDefault();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus {
            GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Number4),
            GeneratedDate = DateTime.Now,
            IsApproved = false,
            OperationId = request.OperationId
        }, Enum.GetName(typeof(Status), Status.Number4Generated));

        return new Number4Dto {
            company = operation.Company,
            operation = operation,
            goods = goods,
            doPayment = payment
        };

    }
}