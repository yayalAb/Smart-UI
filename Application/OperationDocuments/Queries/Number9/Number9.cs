
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;

namespace Application.OperationDocuments.Queries.Number9;

public record Number9 : IRequest<Number9Dto> {
    public int OperationId {get; init;}
    public string Type {get; init;}
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
            GeneratedDocumentName = ((request.Type.ToLower() == "import") ? Enum.GetName(typeof(Documents), Documents.ImportNumber9) : Enum.GetName(typeof(Documents), Documents.TransferNumber9)),
            GeneratedDate = DateTime.Now,
            IsApproved = false,
            OperationId = request.OperationId
        }, ((request.Type.ToLower() == "import") ? Enum.GetName(typeof(Status), Status.ImportNumber9Generated) : Enum.GetName(typeof(Status), Status.TransferNumber9Generated)));

        return new Number9Dto {
            company = operation.Company,
            operation = operation,
            goods = goods,
            doPayment = payment
        };

    }
}