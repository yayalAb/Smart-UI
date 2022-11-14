using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ContainerModule.Queries.GetContainerQuery;

public class GetContainer : IRequest<Container> {
        
    public int Id {get; init;}

    public GetContainer(int id){
        this.Id = id;
    }

}

    public class GetContainerHandler: IRequestHandler<GetContainer, Container> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;

        public GetContainerHandler(
            IIdentityService identityService, 
            IAppDbContext context
        ){
            _identityService = identityService;
            _context = context;
        }

        public async Task<Container> Handle(GetContainer request, CancellationToken cancellationToken) {
            
            var container = await _context.Containers.Include(c => c.Goods).Where(c => c.Id == request.Id ).FirstOrDefaultAsync();
            if(container == null){
                throw new Exception("container not found!");
            }

            return container;

        }

    }