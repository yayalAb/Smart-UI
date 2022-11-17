using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Application.Common.Models;

namespace Application.ContainerModule.Queries.GetAllContainersQuery;

public class GetAllContainers : IRequest<PaginatedList<ContainerDto>> {
    public int? PageCount {set; get;} = 1!;
    public int? PageSize {get; set;} = 10!;
}
public class GetAllContainersHandler: IRequestHandler<GetAllContainers, PaginatedList<ContainerDto>> {

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

    public async Task<PaginatedList<ContainerDto>> Handle(GetAllContainers request, CancellationToken cancellationToken) {
        return await PaginatedList<ContainerDto>.CreateAsync(_context.Containers.Include(c => c.Goods).ProjectTo<ContainerDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
    }

}