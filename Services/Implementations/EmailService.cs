using EduHome.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace EduHome.Services.Implementations
{
    public class EmailService:IEmailService
    {
        public void SendEmail(string email, string link, string emailTitle, string subject, string body)
        {

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("7fvqmgj@code.edu.az", emailTitle);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = subject;
            mailMessage.Body = body;


            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential("7fvqmgj@code.edu.az", "hprw pqtc nttr mypc");
            smtpClient.Send(mailMessage);
        }
    }
}
