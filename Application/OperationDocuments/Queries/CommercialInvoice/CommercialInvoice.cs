
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
    public int docId {get; init;}
}

public class CommercialInvoiceHandler : IRequestHandler<CommercialInvoice, CommercialInvoiceDto> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;

    public CommercialInvoiceHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent) {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
    }

    public async Task<CommercialInvoiceDto> Handle(CommercialInvoice request, CancellationToken cancellationToken) {
        
        var doc = _context.Documentations.Where(d => d.Id == request.docId).Include(d => d.Operation).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.NotFound("Documentaion Not found!"));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == doc.OperationId).ToListAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == doc.OperationId).ToListAsync();

        return new CommercialInvoiceDto {
            Document = doc,
            Operation = doc.Operation,
            Goods = goods,
            Containers = containers
        };

    }
}