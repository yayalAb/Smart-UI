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
    public class GetAllTrucks : IRequest<PaginatedList<TruckDto>> {
        public int PageCount { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetAllTrucksHandler: IRequestHandler<GetAllTrucks, PaginatedList<TruckDto>> {

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

        public async Task<PaginatedList<TruckDto>> Handle(GetAllTrucks request, CancellationToken cancellationToken) {
            return await PaginatedList<TruckDto>.CreateAsync(_context.Trucks.ProjectTo<TruckDto>(_mapper.ConfigurationProvider), request.PageCount, request.PageSize);
        }

    }
}