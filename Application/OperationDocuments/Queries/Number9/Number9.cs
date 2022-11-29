
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Common.PaymentTypes;

namespace Application.OperationDocuments.Queries.Number9;

public record Number9 : IRequest<Number9Dto> {
    public int OperationId {get; set;}
}

public class Number9Handler : IRequestHandler<Number9, Number9Dto>
{
    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public Number9Handler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }
    
    public async Task<Number9Dto> Handle(Number9 request, CancellationToken cancellationToken) {
        
        var operation = _context.Operations.Where(d => d.Id == request.OperationId).FirstOrDefault();
        
        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == request.OperationId).Include(g => g.Container).ToListAsync();

        var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Type == ShippingAgentPaymentType.DeliveryOrder).FirstOrDefault();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        return new Number9Dto {
            operation = operation,
            goods = goods,
            doPayment = payment
        };

    }
}