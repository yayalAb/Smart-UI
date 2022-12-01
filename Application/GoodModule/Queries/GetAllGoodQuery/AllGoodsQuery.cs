using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.GoodModule;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.ContainerModule;

namespace Application.GoodModule.Queries.GetAllGoodQuery {
    public class GetAllGoodQuery : IRequest<PaginatedList<AssignedGoodDto>> {
        public int? PageCount {get; set;} = 1!;
        public int? PageSize {get; set;} = 10!;
    }

    public class GetAllGoodQueryHandler: IRequestHandler<GetAllGoodQuery, PaginatedList<AssignedGoodDto>> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllGoodQueryHandler(
            IIdentityService identityService, 
            IAppDbContext context,
            IMapper mapper
        ){
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AssignedGoodDto>> Handle(GetAllGoodQuery request, CancellationToken cancellationToken) {
            return await PaginatedList<AssignedGoodDto>
            .CreateAsync(_context.Operations
                .Include(o => o.Containers)!
                    .ThenInclude(o => o.Goods)
                        .ThenInclude(g => g.Operation)
                .Include(o => o.Goods)!
                    .ThenInclude(g => g.Operation)
                .Where(o => (o.Containers != null && o.Containers.Count > 0) || (o.Goods != null && o.Goods.Count > 0))
                .Select(o => new AssignedGoodDto{
                    OperationId = o.Id,
                    Containers = _mapper.Map<ICollection<ContainerDto>>(o.Containers),
                    Goods = _mapper.Map<ICollection<FetchGoodDto>>(o.Goods!.Where(g =>g.ContainerId == null))
                }), request.PageCount ?? 1, request.PageSize ?? 10);
        }

    }

}