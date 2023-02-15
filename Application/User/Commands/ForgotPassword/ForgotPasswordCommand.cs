
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Application.User.Commands.ForgotPassword
{
    public record ForgotPasswordCommand : IRequest<CustomResponse>
    {
        public string Email { get; init; }
        public string ClientURI { get; init; }
    }
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, CustomResponse>
    {
        private readonly IIdentityService _identityService;
        // private readonly IEmailService _emailService;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(IIdentityService identityService, ILogger<ForgotPasswordCommandHandler> logger)
        {
            _identityService = identityService;
            // _emailService = emailService;
            _logger = logger;
        }
        public async Task<CustomResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityService.ForgotPassword(request.Email);
            if (!response.result.Succeeded)
            {
                return CustomResponse.Failed(response.result.Errors);
            }
            var token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(response.resetToken)); ;
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
                // await _emailService.SendEmailAsync(mailrequest);
            }
            catch (Exception)
            {
                throw;
            }

            return CustomResponse.Succeeded("successfully sent password reset link by email");

        }
    }
}
