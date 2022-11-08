
using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.Common.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {


        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig )
        {
            _emailConfig = emailConfig;
        }
    
     


        public async Task SendEmailAsync(MailRequest mailRequest)
        {

            var emailMessage = CreateEmailMessage(mailRequest);
            await Send(emailMessage);
            
        }


        private MimeMessage CreateEmailMessage(MailRequest mailRequest)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add( MailboxAddress.Parse(_emailConfig.From));
            emailMessage.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            emailMessage.Subject = mailRequest.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = mailRequest.Body };

            return emailMessage;
        }

        private async Task<bool> Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                   await  client.SendAsync(mailMessage);
                    return true;
                }
                catch
                {
                   
                    throw ;
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
