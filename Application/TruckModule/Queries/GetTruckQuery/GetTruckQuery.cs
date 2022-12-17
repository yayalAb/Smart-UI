using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.TruckModule.Queries.GetTruckQuery
{
    public class GetTruckQuery : IRequest<Truck>
    {

        public int Id { get; init; }

        public GetTruckQuery(int id)
        {
            this.Id = id;
        }

    }

    public class GetTruckQueryHandler : IRequestHandler<GetTruckQuery, Truck>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<GetTruckQueryHandler> _logger;

        public GetTruckQueryHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<GetTruckQueryHandler> logger
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
        }

        public async Task<Truck> Handle(GetTruckQuery request, CancellationToken cancellationToken)
        {

            var truck = await _context.Trucks.Where(t => t.Id == request.Id).FirstOrDefaultAsync();
            if (truck == null)
            {
                throw new GhionException(CustomResponse.NotFound("truck not found!"));
            }

            return truck;

        }

    }
}