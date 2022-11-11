using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TruckModule.Queries.GetTruckQuery
{
    public class GetTruckQuery : IRequest<Truck> {
        
        public int Id {get; init;}

        public GetTruckQuery(int id){
            this.Id = id;
        }

    }

    public class GetTruckQueryHandler: IRequestHandler<GetTruckQuery, Truck> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetTruckQueryHandler> _logger;

        public GetTruckQueryHandler(
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<GetTruckQueryHandler> logger
        ){
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Truck> Handle(GetTruckQuery request, CancellationToken cancellationToken) {
            
            var truck = await _context.Trucks.Include(t => t.Image).Include(t => t.Drivers).Where(t => t.Id == request.Id).FirstOrDefaultAsync();
            if(truck == null){
                throw new Exception("truck not found!");
            }

            return truck;

        }

    }
}