using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Application.Common.Models;
using Application.GoodModule;

namespace Application.ContainerModule.Queries.GetGoodsByLocationQueryQuery;

public class GetGoodsByLocationQuery : IRequest<List<GoodDto>> {
    public int OperationId {set; get;}
    public string Location {get; set;}
}
public class GetGoodsByLocationQueryHandler: IRequestHandler<GetGoodsByLocationQuery, List<GoodDto>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetGoodsByLocationQueryHandler(
        IIdentityService identityService, 
        IAppDbContext context,
        IMapper mapper
    ){
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GoodDto>> Handle(GetGoodsByLocationQuery request, CancellationToken cancellationToken) {
       return await _context.Goods
            .Where(c => c.OperationId == request.OperationId && c.Location == request.Location)
            .ProjectTo<GoodDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

}