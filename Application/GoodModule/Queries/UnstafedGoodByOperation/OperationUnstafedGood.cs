using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GoodModule.Queries.UnstafedGoodByOperation;

public class OperationUnstafedGood : IRequest<ICollection<FetchGoodDto>>
{
    public int OperationId { set; get; }
    public bool Type { get; set; }
}
public class OperationUnstafedGoodHandler : IRequestHandler<OperationUnstafedGood, ICollection<FetchGoodDto>>
{

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public OperationUnstafedGoodHandler(
        IIdentityService identityService,
        IAppDbContext context,
        IMapper mapper
    )
    {
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<FetchGoodDto>> Handle(OperationUnstafedGood request, CancellationToken cancellationToken)
    {
        return request.Type ? await _context.Goods
                        .Where(c => c.OperationId == request.OperationId && (c.ContainerId == null || c.ContainerId == 0))
                        .ProjectTo<FetchGoodDto>(_mapper.ConfigurationProvider).ToListAsync() :
                    await _context.Goods
                        .Where(c => c.OperationId == request.OperationId && (c.ContainerId != null || c.ContainerId == 0))
                        .ProjectTo<FetchGoodDto>(_mapper.ConfigurationProvider).ToListAsync();
    }


}