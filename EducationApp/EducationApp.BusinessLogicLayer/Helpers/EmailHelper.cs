using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class EmailHelper : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string email, string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential();
                credential.UserName = _configuration.GetSection("SMTP")["Email"];
                credential.Password = _configuration.GetSection("SMTP")["Password"];

                client.Credentials = credential;
                client.Host = _configuration.GetSection("SMTP")["Host"];
                client.Port = Int32.Parse(_configuration.GetSection("SMTP")["Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    var mailAddress = new MailAddress(email);
                    emailMessage.To.Add(mailAddress);
                    emailMessage.From = new MailAddress(_configuration.GetSection("SMTP")["Email"]);
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = body;

                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
}