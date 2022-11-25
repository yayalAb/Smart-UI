using Domain.Entities;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationFollowupModule.Queries.GetStatusByOperation;

public record GetStatusByOperation : IRequest<List<OperationStatus>>
{
    public int OperationId {get; init;}

}

public class GetStatusByOperationHandler : IRequestHandler<GetStatusByOperation, List<OperationStatus>>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetStatusByOperationHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<OperationStatus>> Handle(GetStatusByOperation request, CancellationToken cancellationToken)
    {
        return await _context.OperationStatuses.Where(o => o.OperationId == request.OperationId).ToListAsync();
    }

}