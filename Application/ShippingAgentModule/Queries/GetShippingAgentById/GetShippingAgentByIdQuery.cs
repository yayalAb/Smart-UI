
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using AutoMapper;

namespace Application.ShippingAgentModule.Queries.GetShippingAgentById;

public class GetShippingAgentByIdQuery : IRequest<ShippingAgentDto> {
    public int Id {get; set;}

}

public class GetShippingAgentByIdQueryHandler : IRequestHandler<GetShippingAgentByIdQuery, ShippingAgentDto> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetShippingAgentByIdQueryHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShippingAgentDto> Handle(GetShippingAgentByIdQuery request, CancellationToken cancellationToken) {
        
        var agent = await _context.ShippingAgents
        .Include(t => t.Address)
        .FirstOrDefaultAsync(s =>s.Id == request.Id);
        if(agent == null){
            throw new NotFoundException("Shipping Agent" , new{Id=request.Id});
        }

        return agent.ToShippingAgentDto();

    }

}