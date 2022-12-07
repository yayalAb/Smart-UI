
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationDocuments.Queries.Common;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.PackageList;

public record PackageList : IRequest<PackingListDto>
{
    public int operationId { get; init; }
    public int TruckAssignmentId { get; init; }
}

public class PackageListHandler : IRequestHandler<PackageList, PackingListDto>
{

    private readonly IAppDbContext _context;
    private readonly DocumentationService _documentationService;

    public PackageListHandler(IAppDbContext context, DocumentationService documentationService)
    {
        _context = context;
        _documentationService = documentationService;
    }

    public async Task<PackingListDto> Handle(PackageList request, CancellationToken cancellationToken)
    {
        var IsCommercialInvoiceFound = await _context.Documentations
                        .Where(d => d.OperationId == request.operationId && d.Type == Enum.GetName(typeof(Documents), Documents.CommercialInvoice))
                        .AnyAsync();
        if (!IsCommercialInvoiceFound)
        {
            throw new GhionException(CustomResponse.BadRequest("Commercial Invoice document must be generated before packing list"));
        }
        return (PackingListDto)await _documentationService
                     .GetDocumentation(Documents.PackageList, request.operationId, request.TruckAssignmentId, cancellationToken);
    }
}