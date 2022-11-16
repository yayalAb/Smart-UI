using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Application.ContainerModule.Queries.GetAllContainersQuery;

public class GetAllContainers : IRequest<List<ContainerDto>> {}
public class GetAllContainersHandler: IRequestHandler<GetAllContainers, List<ContainerDto>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllContainersHandler(
        IIdentityService identityService, 
        IAppDbContext context,
        IMapper mapper
    ){
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ContainerDto>> Handle(GetAllContainers request, CancellationToken cancellationToken) {
        
        return await _context.Containers.Include(c => c.Goods).ProjectTo<ContainerDto>(_mapper.ConfigurationProvider).ToListAsync();

    }

}