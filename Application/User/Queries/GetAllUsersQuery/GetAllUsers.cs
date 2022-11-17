using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Application.Common.Models;

namespace Application.User.Queries.GetAllUsersQuery;

public class GetAllUsers: IRequest<PaginatedList<IApplicationUser>> {
    public int? PageCount { get; init; } = 1;
    public int? PageSize { get; init; } = 10;
}

public class GetAllUsersHandler: IRequestHandler<GetAllUsers, PaginatedList<IApplicationUser>> {

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly ILogger<GetAllUsersHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(
        IIdentityService identityService, 
        IAppDbContext context,
        ILogger<GetAllUsersHandler> logger,
        IMapper mapper

    ){
        _identityService = identityService;
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginatedList<IApplicationUser>> Handle(GetAllUsers request, CancellationToken cancellationToken) {
        return await PaginatedList<IApplicationUser>.CreateAsync(_identityService.AllUsers().Include(u => u.UserGroup).Include(u => u.Address), request.PageCount ?? 1, request.PageSize ?? 10);
    }

}