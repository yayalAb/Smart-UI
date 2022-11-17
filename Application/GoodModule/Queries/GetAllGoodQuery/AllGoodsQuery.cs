using Domain.Entities;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.GoodModule;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.GoodModule.Queries.GetAllGoodQuery {
    public class GetAllGoodQuery : IRequest<PaginatedList<GoodDto>> {
        public int? PageCount {get; set;} = 1!;
        public int? PageSize {get; set;} = 10!;
    }

    public class GetAllGoodQueryHandler: IRequestHandler<GetAllGoodQuery, PaginatedList<GoodDto>> {

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

        public async Task<PaginatedList<GoodDto>> Handle(GetAllGoodQuery request, CancellationToken cancellationToken) {
            return await PaginatedList<GoodDto>.CreateAsync(_context.Goods.Include(c => c.Container).ProjectTo<GoodDto>(_mapper.ConfigurationProvider), request.PageCount ?? 1, request.PageSize ?? 10);
        }

    }

}