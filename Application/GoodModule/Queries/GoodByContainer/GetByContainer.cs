
using Application.Common.Interfaces;
using Application.GoodModule.Commands.UpdateGoodCommand;
using Application.GoodModule.Queries.GetGoodQuery;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.GoodModule.Queries.GoodByContainer;

public record GetByContainer : IRequest<ICollection<UpdateGoodDto>> {
    public int ContainerId {get; set;}
}

public class GetByContainerHandler : IRequestHandler<GetByContainer, ICollection<UpdateGoodDto>> {

    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;
    private readonly ILogger<GetAssignedGoodQueryHandler> _logger;
    
    public GetByContainerHandler(IMapper mapper, IAppDbContext context, ILogger<GetAssignedGoodQueryHandler> logger) {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public async Task<ICollection<UpdateGoodDto>> Handle(GetByContainer request, CancellationToken cancellationToken) {
        return await _context.Goods.Where(g => g.ContainerId == request.ContainerId).ProjectTo<UpdateGoodDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}