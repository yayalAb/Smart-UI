using System.Net;
using System.Net.Cache;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;

namespace Application.PortModule.Queries.GetAllPortsQuery;

public record GetAllPorts : IRequest<PaginatedList<Port>> {
    public int? PageNumber {get; init;} = 1!;
    public int? PageCount {get; init;} = 10!;
}

public class GetAllDriversHandler : IRequestHandler<GetAllPorts, PaginatedList<Port>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;

    public GetAllDriversHandler(
        IIdentityService identityService, 
        IAppDbContext context
    ){
        _identityService = identityService;
        _context = context;
    }

    public async Task<PaginatedList<Port>> Handle(GetAllPorts request, CancellationToken cancellationToken) {
        return await PaginatedList<Port>.CreateAsync(_context.Ports, request.PageNumber ?? 1, request.PageCount ?? 10);
    }

}