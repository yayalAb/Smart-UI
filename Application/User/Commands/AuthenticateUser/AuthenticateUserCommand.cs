using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.User.Commands.AuthenticateUser
{
    public record AuthenticateUserCommand : IRequest<string>
    {
        public string Email { get; init; }    
        public string Password { get; init; }

    }
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
    {
        private readonly IIdentityService _identityService;

        public AuthenticateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _identityService.AuthenticateUser(request.Email, request.Password);
                if (!response.result.Succeeded)
                {

                    throw new InvalidLoginException(string.Join(",", response.result.Errors));
                }
                return response.tokenString;

            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
