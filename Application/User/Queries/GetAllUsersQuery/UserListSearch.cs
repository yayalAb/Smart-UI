using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.User.Queries.GetAllUsersQuery;

public class UserListSearch : IRequest<PaginatedList<UserDto>>
{
    public string Word { get; init; }
    public int? PageCount { get; init; } = 1;
    public int? PageSize { get; init; } = 10;
}

public class UserListSearchHandler : IRequestHandler<UserListSearch, PaginatedList<UserDto>>
{

    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly ILogger<UserListSearchHandler> _logger;
    private readonly IMapper _mapper;

    public UserListSearchHandler (
        IIdentityService identityService,
        IAppDbContext context,
        ILogger<UserListSearchHandler> logger,
        IMapper mapper

    )
    {
        _identityService = identityService;
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserDto>> Handle(UserListSearch request, CancellationToken cancellationToken)
    {
        return await PaginatedList<UserDto>.CreateAsync(
            _identityService.AllUsers()
                .Include(u => u.UserGroup)
                .Include(u => u.Address)
                .Where(u => u.UserName.Contains(request.Word) ||
                    u.Email.Contains(request.Word) ||
                    u.FullName.Contains(request.Word) ||
                    u.Address.Phone.Contains(request.Word) ||
                    u.UserGroup.Name.Contains(request.Word)
                )
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FullName = u.FullName,
                    PhoneNumber = u.Address.Phone,
                    UserGroupName = u.UserGroup.Name
                }), request.PageCount ?? 1, request.PageSize ?? 10);
    }

}