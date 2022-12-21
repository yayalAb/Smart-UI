using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.TruckModule.Queries.GetAllTruckQuery
{
    public class TruckListSearch : IRequest<PaginatedList<TruckDto>>
    {
        public string Word { get; init; }
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class TruckListSearchHandler : IRequestHandler<TruckListSearch, PaginatedList<TruckDto>>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public TruckListSearchHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<GetAllTrucksHandler> logger,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TruckDto>> Handle(TruckListSearch request, CancellationToken cancellationToken)
        {
            return await PaginatedList<TruckDto>.CreateAsync(
                _context.Trucks
                    .Where(t => t.TruckNumber.Contains(request.Word) ||
                        t.Type.Contains(request.Word) ||
                        (t.Capacity != null ? t.Capacity.ToString().Contains(request.Word) : false) ||
                        t.PlateNumber.Contains(request.Word)
                    )
                    .ProjectTo<TruckDto>(_mapper.ConfigurationProvider), 
                request.PageCount, 
                request.PageSize
            );
        }

    }
}