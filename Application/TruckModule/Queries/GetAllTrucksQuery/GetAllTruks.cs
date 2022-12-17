using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.TruckModule.Queries.GetAllTruckQuery
{
    public class GetAllTrucks : IRequest<PaginatedList<TruckDto>>
    {
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllTrucksHandler : IRequestHandler<GetAllTrucks, PaginatedList<TruckDto>>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTrucksHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<GetAllTrucksHandler> logger,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TruckDto>> Handle(GetAllTrucks request, CancellationToken cancellationToken)
        {
            return await PaginatedList<TruckDto>.CreateAsync(_context.Trucks.ProjectTo<TruckDto>(_mapper.ConfigurationProvider), request.PageCount, request.PageSize);
        }

    }
}