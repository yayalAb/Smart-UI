using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.User.Commands.AuthenticateUser
{
    public record AuthenticateUserCommand : IRequest<LoginResponse>
    {
        public string Email { get; init; }    
        public string Password { get; init; }

    }
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, LoginResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<AuthenticateUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService emailService;

        public AuthenticateUserCommandHandler(IIdentityService identityService ,IAppDbContext context, ILogger<AuthenticateUserCommandHandler>logger , IMapper mapper , IEmailService emailService)
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
            this.emailService = emailService;
        }
        public async Task<LoginResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            try {

                // authenticating user
                var response = await _identityService.AuthenticateUser(request.Email, request.Password);

                if (!response.result.Succeeded) {
                    throw new InvalidLoginException(string.Join(",", response.result.Errors));
                }
                IApplicationUser user = response.user;
               // fetching user roles
                IEnumerable<UserRoleDto> roles = _context.AppUserRoles
                    .Where(r => r.UserGroupId.Equals(response.user.UserGroupId))
                    .ProjectTo<UserRoleDto>(_mapper.ConfigurationProvider);
                 
                return new LoginResponse {
                    fullName = user.FullName,
                    id=user.Id ,
                    roles=roles ,
                    tokenString = response.tokenString ,    
                    userGroupId = user.UserGroupId ,
                    userName = user.UserName ,
                };

            } catch (Exception) {
                throw;
            }
           
        }
    }
}
