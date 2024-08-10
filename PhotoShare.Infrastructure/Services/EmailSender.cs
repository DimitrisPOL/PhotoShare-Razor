using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using PhotoShare.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace PhotoShare.Infrastructure.Services
{
   public interface IEmaiSender
    {
        public void Email(string htmlString, string from, string to, string subject);
    }
    public class EmailSender : IEmaiSender
    {
        private ApplicationConfiguration Configuration;
        public EmailSender(IOptions<ApplicationConfiguration> optionsSnapshot)
        {
            Configuration = optionsSnapshot.Value;
        }
        public void Email(string htmlString, string from, string to, string subject)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(Configuration.Settings.EmailSettings.Email);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Configuration.Settings.EmailSettings.Email, Configuration.Settings.EmailSettings.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
