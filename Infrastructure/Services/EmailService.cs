
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        private readonly IAppDbContext _context;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IAppDbContext context , ILogger<EmailService> logger)
        {
            _context = context;
            _logger = logger;
        }




        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var emailConfig = await _context.Settings.FirstOrDefaultAsync();
            if (emailConfig == null)
            {
                throw new Exception("email configuration not found");
            }
            var emailMessage = CreateEmailMessage(mailRequest, emailConfig);
            try
            {
                
            await Send(emailMessage, emailConfig);
            }
            catch 
            {
                
                throw new GhionException(CustomResponse.Failed("server time out while connecting to smtp server for sending email, check server configuration and ports"));
            }

        }


        private MimeMessage CreateEmailMessage(MailRequest mailRequest, Setting emailConfig)
        {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(emailConfig.Email));
            emailMessage.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            emailMessage.Subject = mailRequest.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = mailRequest.Body };

            return emailMessage;
        }

        private async Task<bool> Send(MimeMessage mailMessage, Setting emailConfig)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailConfig.Host, emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.Username, emailConfig.Password);

                    await client.SendAsync(mailMessage);
                    return true;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

    }
}
