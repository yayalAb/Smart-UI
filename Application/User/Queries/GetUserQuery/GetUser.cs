using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Queries.GetUserQuery;

public record GetUser : IRequest<IApplicationUser>
{
    public string Id { get; set; }
}

public class GetUserHandler : IRequestHandler<GetUser, IApplicationUser>
{
    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetUserHandler(
        IIdentityService identityService,
        IAppDbContext context,
        IMapper mapper

    )
    {
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IApplicationUser> Handle(GetUser request, CancellationToken cancellationToken)
    {
        var user = await _identityService.AllUsers().Include(u => u.UserGroup).Include(u => u.Address).Where(u => u.Id == request.Id).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new GhionException(CustomResponse.NotFound("user not found!"));
        }
        return user;
    }
}