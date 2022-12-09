using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationDocuments.Queries.T1Document.T1Dtos;
using Application.OperationFollowupModule;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    private readonly DefaultCompanyService _defaultCompanyService;

    public T1DocumentHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent, DefaultCompanyService defaultCompanyService) {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
        _defaultCompanyService = defaultCompanyService;
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
                    var operation = await _context.Operations.Where(o => o.Id == request.OperationId).ProjectTo<T1OperationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("There is no Operation with the given Id!"));
                    }
                    else if (!await _operationEvent.IsDocumentApproved(request.OperationId, Enum.GetName(typeof(Documents), Documents.Number4)!))
                    {
                        throw new GhionException(CustomResponse.NotFound("Number 4 Document should be approved!"));
                    }

                    List<T1TruckAssignmentDto> truckAssignment = await _context.TruckAssignments
                                            .Where(d => d.OperationId == request.OperationId)
                                            .Include(t => t.Truck)
                                            .Include(t => t.Goods)
                                            .Include(t => t.Containers)
                                            .Include(t => t.SourcePort)
                                            .Select(t => new T1TruckAssignmentDto {
                                                AssignedTruck = new T1TruckDto {
                                                    TruckNumber = t.Truck.TruckNumber
                                                },
                                                AssignedGood = (t.Goods != null) ? t.Goods.Select(g => new T1GoodDto {
                                                    HSCode = g.HSCode,
                                                    Weight = g.Weight,
                                                    Quantity = g.Quantity,
                                                    ChasisNumber = g.ChasisNumber,
                                                    Unit = g.Unit,
                                                    UnitPrice = g.UnitPrice
                                                }).ToList() : null
                                            }).ToListAsync();

                    if (truckAssignment.Count == 0) {
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
                    return new T1DocumentDto {
                        TruckAssignments = (from assignments in truckAssignment where assignments.AssignedGood != null && assignments.AssignedGood.Count > 0 select assignments).ToList(),
                        Operation = operation,
                        DefaultCompanyInformation = await _defaultCompanyService.GetDefaultCompanyAsync()
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