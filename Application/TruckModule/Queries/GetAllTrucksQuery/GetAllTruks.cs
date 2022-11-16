using System;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Application.Common.Models;

namespace Application.TruckModule.Queries.GetAllTruckQuery
{
    public class GetAllTrucks : IRequest<List<TruckDto>> {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllTrucksHandler: IRequestHandler<GetAllTrucks, List<TruckDto>> {

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

        public async Task<List<TruckDto>> Handle(GetAllTrucks request, CancellationToken cancellationToken) {
            
            var trucks = await PaginatedList<TruckDto>.CreateAsync(_context.Trucks.ProjectTo<TruckDto>(_mapper.ConfigurationProvider), request.PageNumber, request.PageSize);
            return trucks.Items;

        }

    }
}