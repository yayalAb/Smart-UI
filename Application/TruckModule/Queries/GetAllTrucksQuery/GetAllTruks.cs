using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Application.TruckModule.Queries.GetAllTruckQuery
{
    public class GetAllTrucks : IRequest<ICollection<TruckDto>> {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllTrucksHandler: IRequestHandler<GetAllTrucks, ICollection<TruckDto>> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetAllTrucksHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllTrucksHandler(
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<GetAllTrucksHandler> logger,
            IMapper mapper
        ){
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ICollection<TruckDto>> Handle(GetAllTrucks request, CancellationToken cancellationToken) {
            
            var truck = await _context.Trucks.Include(t => t.Image).ProjectTo<TruckDto>(_mapper.ConfigurationProvider).ToListAsync();
            if(truck == null){
                throw new Exception("truck not found!");
            }

            return truck;
            // throw new Exception("not implemented yet!");

        }

    }
}