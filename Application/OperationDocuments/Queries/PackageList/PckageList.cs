
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.PackageList;

public record PackageList : IRequest<PackageListDto> {
    public int documentationId {get; init;}
}

public class PackageListHandler : IRequestHandler<PackageList, PackageListDto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;

    public PackageListHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent) {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
    }

    public async Task<PackageListDto> Handle(PackageList request, CancellationToken cancellationToken) {

        var doc = _context.Documentations.Where(d => d.Id == request.documentationId).Include(d => d.Operation).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.NotFound("Documentaion Not found!"));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == doc.OperationId).ToListAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == doc.OperationId).ToListAsync();

        // _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus {
        //     GeneratedDocumentName = "Package List",
        //     GeneratedDate = DateTime.Now,
        //     IsApproved = true,
        //     OperationId = doc.OperationId
        // });

        return new PackageListDto {
            documentation = doc,
            operation = doc.Operation,
            goods = goods,
            containers = containers
        };

    }
}