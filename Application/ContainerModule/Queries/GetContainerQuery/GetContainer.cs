using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Application.ContainerModule.Queries.GetContainerQuery;

public class GetContainer : IRequest<ContainerDto> {
        
    public int Id {get; init;}

    public GetContainer(int id){
        this.Id = id;
    }

}

    public class GetContainerHandler: IRequestHandler<GetContainer, ContainerDto> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetContainerHandler(
            IIdentityService identityService, 
            IAppDbContext context,
            IMapper mapper
        ){
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContainerDto> Handle(GetContainer request, CancellationToken cancellationToken) {
            
            var container = await _context.Containers.Include(c => c.Goods).ProjectTo<ContainerDto>(_mapper.ConfigurationProvider).Where(c => c.Id == request.Id ).FirstOrDefaultAsync();
            if(container == null){
                throw new Exception("container not found!");
            }

            return container;

        }

    }