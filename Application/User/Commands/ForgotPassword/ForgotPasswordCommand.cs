
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Web;

namespace Application.User.Commands.ForgotPassword
{
    public record ForgotPasswordCommand : IRequest<bool>
    {
        public string Email { get; init; }   
        public string ClientURI { get; init; }
    }
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(IIdentityService identityService , IEmailService emailService , ILogger<ForgotPasswordCommandHandler> logger)
        {
           _identityService = identityService;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.ForgotPassword(request.Email);
            if (!response.result.Succeeded)
            {
                return false;

            }
            var token  = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(response.resetToken)); ;
            var param = new Dictionary<string, string?>
            {
                { "token" , token },
                { "email" , request.Email }
            };

            var callback = QueryHelpers.AddQueryString(request.ClientURI, param);
            var emailContent = "Please use the link below to reset your password" + callback;
            

            // sending  password reset email 
            var mailrequest = new MailRequest()
            {
                Subject = "Reset Password",
                Body = emailContent,
                ToEmail = request.Email,
            };
            try
            {
                await _emailService.SendEmailAsync(mailrequest);
            }
            catch (Exception)
            {

                throw;
            }
            return true;

        }
    }
}
