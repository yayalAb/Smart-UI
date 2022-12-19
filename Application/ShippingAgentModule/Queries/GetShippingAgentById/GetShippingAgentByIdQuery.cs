
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentById;

public class GetShippingAgentByIdQuery : IRequest<SingleShippingAgentDto>
{
    public int Id { get; set; }

}

public class GetShippingAgentByIdQueryHandler : IRequestHandler<GetShippingAgentByIdQuery, SingleShippingAgentDto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SingleShippingAgentDto> Handle(GetShippingAgentByIdQuery request, CancellationToken cancellationToken)
    {

        var agent = await _context.ShippingAgents
        .Include(t => t.Address)
        .ProjectTo<SingleShippingAgentDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(s => s.Id == request.Id);
        if (agent == null)
        {
            throw new GhionException(CustomResponse.NotFound("Shipping Agent not found"));
        }

        return agent;

    }

}