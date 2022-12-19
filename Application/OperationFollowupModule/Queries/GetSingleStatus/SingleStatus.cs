using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.OperationFollowupModule.Queries.GetSingleStatus;

public record SingleStatus : IRequest<OperationStatus>
{
    public int Id { get; init; }
}

public class SingleStatusHandler : IRequestHandler<SingleStatus, OperationStatus>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public SingleStatusHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationStatus> Handle(SingleStatus request, CancellationToken cancellationToken)
    {

        var status = await _context.OperationStatuses.FindAsync(request.Id);

        if (status == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation status not found!"));
        }

        return status;

    }

}