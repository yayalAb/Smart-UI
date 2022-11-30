
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.PackageList;

public record PackageList : IRequest<PackageListDto> {
    public int operationId {get; init;}
}

public class PackageListHandler : IRequestHandler<PackageList, PackageListDto> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public PackageListHandler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<PackageListDto> Handle(PackageList request, CancellationToken cancellationToken) {

        var operation = _context.Operations.Where(d => d.Id == request.operationId).FirstOrDefault();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var doc = _context.Documentations.Where(d => d.OperationId == request.operationId && d.Type == Enum.GetName(typeof(Documents), Documents.PackageList)).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == doc.OperationId).Include(g => g.Container).ToListAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == doc.OperationId).ToListAsync();

        return new PackageListDto {
            documentation = doc,
            operation = operation,
            goods = goods,
            containers = containers
        };

    }
}