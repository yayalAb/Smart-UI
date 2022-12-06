
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;
using Application.OperationDocuments.Queries.Common;

namespace Application.OperationDocuments.Queries.TruckWayBill;

public record TruckWayBill : IRequest<DocsDto>
{
    public int operationId { get; init; }
    public int TruckAssignmentId { get; init; }
    public bool isWaybill { get; init; } = false!;
}

public class TruckWayBillHandler : IRequestHandler<TruckWayBill, DocsDto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;
    private readonly DocumentationService _documentationService;

    public TruckWayBillHandler(IAppDbContext context, OperationEventHandler operationEvent, DocumentationService documentationService)
    {
        _context = context;
        _operationEvent = operationEvent;
        _documentationService = documentationService;
    }

    public async Task<DocsDto> Handle(TruckWayBill request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    var operation = _context.Operations.Where(d => d.Id == request.operationId).FirstOrDefault();

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }
                    //if truck waybill
                    if (!request.isWaybill)
                    {
                        return await _documentationService
                                            .GetDocumentation(Documents.TruckWayBill, request.operationId, request.TruckAssignmentId, cancellationToken);
                    }
                    //if waybill
                    if (!await _operationEvent.IsDocumentGenerated(request.operationId, Enum.GetName(typeof(Documents), Documents.ECDDocument)!))
                    {
                        throw new GhionException(CustomResponse.NotFound("ECD Document should be dispatched before generating waybill!"));
                    }
                     await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Waybill)!,
                        GeneratedDate = DateTime.Now,
                        IsApproved = false,
                        OperationId = request.operationId
                    }, Enum.GetName(typeof(Status), Status.Closed)!);
                    var document = await _documentationService
                                    .GetDocumentation(Documents.Waybill, request.operationId, request.TruckAssignmentId, cancellationToken);
                  await transaction.CommitAsync();
                  return document;
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