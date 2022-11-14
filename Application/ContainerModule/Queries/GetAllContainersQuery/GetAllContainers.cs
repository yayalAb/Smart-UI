using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContainerModule.Queries.GetAllContainersQuery;

public class GetAllContainers : IRequest<List<Container>> {}
public class GetAllContainersHandler: IRequestHandler<GetAllContainers, List<Container>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllContainersHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<List<Container>> Handle(GetAllContainers request, CancellationToken cancellationToken) {
        
        return await _context.Containers.Include(c => c.Goods).ToListAsync();

    }

}