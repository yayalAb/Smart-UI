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

        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    var operation = await _context.Operations.FindAsync(request.OperationId);

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("There is no Operation with the given Id!"));
                    }
                    else if (!await _operationEvent.IsDocumentApproved(request.OperationId , Enum.GetName(typeof(Documents) , Documents.Number4)!))
                    {
                        throw new GhionException(CustomResponse.NotFound("Number 4 Document should be approved!"));
                    }

                    List<TruckAssignment> truckAssignment = await _context.TruckAssignments
                                            .Where(d => d.OperationId == request.OperationId)
                                            .Include(t => t.Truck)
                                            .Include(t => t.Goods)
                                            .Include(t => t.Containers)
                                            .Include(t => t.SourcePort)
                                            .Include(t => t.DestinationPort)
                                            .Select(t => new TruckAssignment
                        {
                            DriverId = t.DriverId,
                            TruckId = t.TruckId,
                            OperationId = t.OperationId,
                            SourceLocation = t.SourceLocation,
                            DestinationLocation = t.DestinationLocation,
                            SourcePortId = t.SourcePortId,
                            DestinationPortId = t.DestinationPortId,
                            Truck = new Truck
                            {
                                TruckNumber = t.Truck.TruckNumber,
                                Type = t.Truck.Type,
                                PlateNumber = t.Truck.PlateNumber,
                                Capacity = t.Truck.Capacity,
                                IsAssigned = t.Truck.IsAssigned
                            },
                            Containers = (t.Containers != null) ? t.Containers.Select(c => new Container()
                            {
                                ContianerNumber = c.ContianerNumber,
                                SealNumber = c.SealNumber,
                                Location = c.Location,
                                Size = c.Size,
                                LocationPortId = c.LocationPortId,
                                IsAssigned = c.IsAssigned,
                                OperationId = c.OperationId
                            }).ToList() : null,
                            Goods = (t.Goods != null) ? t.Goods.Select(g => new Good
                            {
                                Description = g.Description,
                                HSCode = g.HSCode,
                                Manufacturer = g.Manufacturer,
                                Weight = g.Weight,
                                Quantity = g.Quantity,
                                NumberOfPackages = g.NumberOfPackages,
                                Type = g.Type,
                                Location = g.Location,
                                ChasisNumber = g.ChasisNumber,
                                EngineNumber = g.EngineNumber,
                                ModelCode = g.ModelCode,
                                IsAssigned = g.IsAssigned,
                                ContainerId = g.ContainerId,
                                OperationId = g.OperationId,
                                LocationPortId = g.LocationPortId
                            }).ToList() : null
                        }).ToListAsync();

                    if (truckAssignment.Count == 0)
                    {
                        throw new GhionException(CustomResponse.NotFound("There is no Truck Assignment!"));
                    }

                    await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.T1)!,
                        GeneratedDate = DateTime.Now,
                        IsApproved = false,
                        OperationId = request.OperationId
                    }, Enum.GetName(typeof(Status), Status.T1Generated)!);
                    await transaction.CommitAsync();
                    return new T1DocumentDto
                    {
                        TruckAssignments = truckAssignment,
                        Operation = operation
                    };

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