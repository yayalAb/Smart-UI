using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.User.Commands.CreateUser
{

    public record CreateUserCommand : IRequest<string>
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public int GroupId { get; init; }
        public List<UserRoleDto>? UserRoles { get; init; }

    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IIdentityService identityService, IAppDbContext context, ILogger<CreateUserCommandHandler> logger, IMapper mapper)
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            List<AppUserRole> userRoles = _mapper.Map<List<AppUserRole>>(request.UserRoles);

            using var transaction = _context.database.BeginTransaction();

            var response = await _identityService.createUser(request.FullName, request.UserName, request.Email, request.Password, request.GroupId);

            if (!response.result.Succeeded)
            {
                throw new CantCreateUserException(response.result.Errors.ToList());
            }

            if (userRoles == null || userRoles.ToArray().Length == 0)
            {
                userRoles = AppUserRole.createDefaultRoles(response.userId);
            }
            if (userRoles.ToArray().Length != Enum.GetNames(typeof(Page)).Length)
            {
                userRoles = AppUserRole.fillUndefinedRoles(userRoles);
            }
            //add user id to every role
            for (int i = 0; i < userRoles.Count; i++)
            {

                if (userRoles[i].ApplicationUserId == null)
                {
                    userRoles[i].ApplicationUserId = response.userId;
                }

            }

            try
            {

                await _context.AddRangeAsync(userRoles);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return response.userId;

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new CantCreateUserException(new List<string> { e.Message });
            }

        }
    }
}
