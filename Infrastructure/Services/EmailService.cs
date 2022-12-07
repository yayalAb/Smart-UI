
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Common.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {

        private readonly IAppDbContext _context;

        public EmailService(IAppDbContext context)
        {
            _context = context;
        }




        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var emailConfig = await _context.Settings.FirstOrDefaultAsync();
            if(emailConfig == null ){
                throw new Exception("email configuration not found");
            }
            var emailMessage =  CreateEmailMessage(mailRequest, emailConfig);
            await Send(emailMessage, emailConfig);

        }


        private  MimeMessage CreateEmailMessage(MailRequest mailRequest, Setting emailConfig)
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
