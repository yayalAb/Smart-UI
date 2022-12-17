using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.GoodModule.Queries.GetAllGoodQuery
{
    public class GetAllGoodQuery : IRequest<PaginatedList<FetchGoodDto>>
    {
        public int? PageCount { get; set; } = 1!;
        public int? PageSize { get; set; } = 10!;
    }

    public class GetAllGoodQueryHandler : IRequestHandler<GetAllGoodQuery, PaginatedList<FetchGoodDto>>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllGoodQueryHandler(
            IIdentityService identityService,
            IAppDbContext context,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FetchGoodDto>> Handle(GetAllGoodQuery request, CancellationToken cancellationToken)
        {
            return await PaginatedList<FetchGoodDto>
            .CreateAsync(
                _context.Goods
                    .Include(g => g.Container)
                    .Include(g => g.Operation)
                    .ProjectTo<FetchGoodDto>(_mapper.ConfigurationProvider)
               , request.PageCount ?? 1, request.PageSize ?? 10);
        }

    }

}