
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;

namespace Application.OperationDocuments.Queries.TruckWayBill;

public record TruckWayBill : IRequest<TruckWayBillDto>
{
    public int operationId { get; init; }
    public int TruckAssignmentId { get; init; }
    public bool Type {get; init;} = false!;
}

public class TruckWayBillHandler : IRequestHandler<TruckWayBill, TruckWayBillDto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public TruckWayBillHandler(IAppDbContext context, OperationEventHandler operationEvent)
    {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<TruckWayBillDto> Handle(TruckWayBill request, CancellationToken cancellationToken)
    {

        var operation = _context.Operations.Where(d => d.Id == request.operationId).FirstOrDefault();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }else if(request.Type && operation.Status != Enum.GetName(typeof(Status), Status.ECDDispatched)){
            throw new GhionException(CustomResponse.NotFound("ECD Document should be dispached!"));
        }

        Documentation doc = null;
        if(!request.Type){
            
            doc = _context.Documentations.Where(d => d.OperationId == request.operationId).FirstOrDefault();
            if (doc == null) {
                throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
            }

        }

        var assignment = await _context.TruckAssignments
                                        .Include(t => t.Driver)
                                        .Include(t => t.Truck)
                                        .Include(t => t.SourcePort)
                                        .Include(t => t.DestinationPort)
                                        .Include(t => t.Containers)
                                        .Include(t => t.Goods)
                                        .Where(g => g.Id == request.TruckAssignmentId)
                                        .FirstAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == request.operationId).ToListAsync();

        if (assignment == null) {
            throw new GhionException(CustomResponse.NotFound("Assignment not Found!"));
        }

        TruckWayBillDto bill = new TruckWayBillDto {
            documentation = doc,
            operation = operation
        };

        if (assignment.Containers != null) {
            bill.containers = assignment.Containers;
        }

        if (assignment.Goods != null) {
            bill.goods = assignment.Goods;
        }
        
        if(request.Type){
            await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus {
                GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Waybill),
                GeneratedDate = DateTime.Now,
                IsApproved = true,
                OperationId = doc.OperationId
            }, Enum.GetName(typeof(Status), Status.Closed));
        }

        return bill;

    }
}