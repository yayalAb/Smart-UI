
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.CertificateOfOrigin;

public record CertificateOfOrigin : IRequest<CertificateDto> {
    public int operationId {get; set;}
}

public class CertificateOfOriginHandler : IRequestHandler<CertificateOfOrigin, CertificateDto> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public CertificateOfOriginHandler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }
    
    public async Task<CertificateDto> Handle(CertificateOfOrigin request, CancellationToken cancellationToken) {
        
        var operation = _context.Operations.Where(d => d.Id == request.operationId).Include(o => o.Company).FirstOrDefault();

        if(operation == null) {
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == request.operationId).Include(g => g.Container).ToListAsync();

        return new CertificateDto {
            operation = operation,
            goods = goods
        };

    }
}