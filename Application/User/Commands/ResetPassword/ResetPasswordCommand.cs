

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Web;

namespace Application.User.Commands.ResetPassword
{
    public record ResetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }   
    }
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IIdentityService identityService;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IIdentityService identityService , ILogger<ResetPasswordCommandHandler> logger)
        {
            this.identityService = identityService;
            _logger = logger;
        }
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var response = await identityService.ResetPassword(request.Email, request.Password, code);
            if(!response.Succeeded)
            {
                throw new PasswordResetException(string.Join(",", response.Errors));
            }
            return true;
        }
    }
}
