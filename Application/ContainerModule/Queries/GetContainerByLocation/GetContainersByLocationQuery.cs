using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Application.Common.Models;

namespace Application.ContainerModule.Queries.GetContainersByLocationQueryQuery;

public class GetContainersByLocationQuery : IRequest<List<ContainerDto>> {
    public int OperationId {set; get;}
    public string Location {get; set;}
}
public class GetContainersByLocationQueryHandler: IRequestHandler<GetContainersByLocationQuery, List<ContainerDto>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetContainersByLocationQueryHandler(
        IIdentityService identityService, 
        IAppDbContext context,
        IMapper mapper
    ){
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ContainerDto>> Handle(GetContainersByLocationQuery request, CancellationToken cancellationToken) {
       return await _context.Containers
            .Where(c => c.OperationId == request.OperationId && c.Location == request.Location)
            .Include(c => c.Goods)
            .ProjectTo<ContainerDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

}