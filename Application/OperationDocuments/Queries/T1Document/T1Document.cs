
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.T1Document;

public record T1Document : IRequest<T1DocumentDto>
{
    public int OperationId { get; init; }
}

public class T1DocumentHandler : IRequestHandler<T1Document, T1DocumentDto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;

    public T1DocumentHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent)
    {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
    }

    public async Task<T1DocumentDto> Handle(T1Document request, CancellationToken cancellationToken)
    {

        var operation = await _context.Operations.FindAsync(request.OperationId);

        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("There is no Operation with the given Id!"));
        }

        List<TruckAssignment> truckAssignment = await _context.TruckAssignments
                            .Where(d => d.OperationId == request.OperationId)
                            .Include(t => t.Truck)
                            .Include(t => t.Containers)
                            .Include(t => t.Goods)
                            .ToListAsync();

        if (truckAssignment.Count == 0)
        {
            throw new GhionException(CustomResponse.NotFound("There is no Truck Assignment!"));
        }
        var statusName = Enum.GetName(typeof(Status), Status.T1Generated);
       await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
        {
            GeneratedDocumentName = "T1 Document",
            GeneratedDate = DateTime.Now,
            IsApproved = false,
            OperationId = request.OperationId
        },
        statusName!
        );

        return new T1DocumentDto
        {
            TruckAssignments = truckAssignment,
            Operation = operation
        };

    }
}