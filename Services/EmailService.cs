using System;
// using System.Threading.Tasks;
// using miprimerAPI.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using test2.Entities;
// using Microsoft.AspNetCore.Mvc;

namespace test2.Services
{
    public interface IEmailService
    {
        bool SendEmail(Email email);
    }
    public class EmailService : IEmailService
    {
        public bool SendEmail(Email email)
        {
            try {

            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Andres Sosa", "andres.sosa@medmax.site");
            MailboxAddress to = new MailboxAddress(email.To);
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = email.Subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            var body = $"<h1>Hola {email.To}</h1><br><p>Gracias por Realizar su compra<p>";
            if(email.Body != null) {
                body = email.Body;
            }
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("server118.web-hosting.com", 465, true);
            client.Authenticate("andres.sosa@medmax.site", "sosadiaz20**");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return true;
            } catch (Exception e) {
                return false;
            }
            
        }

    }
}