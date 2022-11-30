
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public record CommercialInvoice : IRequest<CommercialInvoiceDto> {
    public int operationId {get; init;}
    //if type is false it means Commercia invoice if it is true it means Proforma invoice
    public bool Type {get; init;} = false;
}

public class CommercialInvoiceHandler : IRequestHandler<CommercialInvoice, CommercialInvoiceDto> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public CommercialInvoiceHandler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<CommercialInvoiceDto> Handle(CommercialInvoice request, CancellationToken cancellationToken) {
        
        var operation = _context.Operations.Where(d => d.Id == request.operationId).FirstOrDefault();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var doc = _context.Documentations.Where(d => d.OperationId == request.operationId && d.Type == Enum.GetName(typeof(Documents), !request.Type ? Documents.CommercialInvoice : Documents.ProformaInvoice)).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == doc.OperationId).Include(g => g.Container).ToListAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == doc.OperationId).ToListAsync();

        return new CommercialInvoiceDto {
            Document = doc,
            Operation = operation,
            Goods = goods,
            Containers = containers
        };

    }
}