using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TruckAssignmentModule.Queries.GetTruckAssignmentById;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TruckAssignmentModule.Queries;
public record GetTruckAssignmentByIdQuery : IRequest<TruckAssignmentByIdDto>
{
    public int Id { get; set; }
}
public class GetTruckAssignmentsByIdQueryHandler : IRequestHandler<GetTruckAssignmentByIdQuery,TruckAssignmentByIdDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTruckAssignmentsByIdQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<TruckAssignmentByIdDto> Handle(GetTruckAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
       var truckAssignment = await _context.TruckAssignments
        .Include(ta => ta.Containers)
        .Include(ta => ta.Goods.Where(g => g.ContainerId == null))
        .Where(ta => ta.Id == request.Id)
        .Select(ta => new TruckAssignmentByIdDto{
            Id = ta.Id,
            OperationId = ta.OperationId,
            DriverId = ta.DriverId,
            TruckId = ta.TruckId,
            SourceLocation = ta.SourceLocation,
            DestinationLocation = ta.DestinationLocation,
            SourcePortId = ta.SourcePortId,
            DestinationPortId = ta.DestinationPortId,
            TransportationMethod = ta.TransportationMethod,
            AgreedTariff = ta.AgreedTariff,
            Currency = ta.Currency,
            SENumber = ta.SENumber,
            Date = ta.Date,
            GatePassType = ta.GatePassType,
            ContainerIds = ta.Containers.Select(c => c.Id).ToList(),
            GoodIds = ta.Goods.Select(g => g.Id).ToList()
        })
        .FirstOrDefaultAsync();
        if(truckAssignment == null){
            throw new GhionException(CustomResponse.NotFound($"truck assignment with id {request.Id} is not found"));
        }
        return truckAssignment ;
    }
}
