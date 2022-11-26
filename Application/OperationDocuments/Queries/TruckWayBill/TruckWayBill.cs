
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Application.OperationFollowupModule;
using Domain.Entities;

namespace Application.OperationDocuments.Queries.TruckWayBill;

public record TruckWayBill : IRequest<TruckWayBillDto> {
    public int documentationId {get; init;}
    public int TruckAssignmentId {get; init;}
}

public class TruckWayBillHandler : IRequestHandler<TruckWayBill, TruckWayBillDto> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;

    public TruckWayBillHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent) {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
    }
    
    public async Task<TruckWayBillDto> Handle(TruckWayBill request, CancellationToken cancellationToken)
    {
        var doc = _context.Documentations.Where(d => d.Id == request.documentationId).Include(d => d.Operation).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.NotFound("Documentaion Not found!"));
        }

        var assignment = await _context.TruckAssignments
                                        .Include(t => t.Driver)
                                        .Include(t => t.Truck)
                                        .Include(t => t.SourcePort)
                                        .Include(t => t.DestinationPort)
                                        .Include(t => t.Containers)
                                        .Include(t => t.Goods)
                                        .Where(g => g.Id == request.TruckAssignmentId).FirstAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == doc.OperationId).ToListAsync();

        if(assignment == null){
            throw new GhionException(CustomResponse.NotFound("Assignment not Found!"));
        }

        TruckWayBillDto bill = new TruckWayBillDto {
            documentation = doc,
            operation = doc.Operation
        };

        if(assignment.Containers != null){
            bill.containers = assignment.Containers;
        }

        if(assignment.Goods != null){
            bill.goods = assignment.Goods;
        }

        _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus {
            GeneratedDocumentName = "Truck Way Bill",
            GeneratedDate = DateTime.Now,
            IsApproved = false,
            OperationId = doc.OperationId
        });

        return bill;
        
    }
}