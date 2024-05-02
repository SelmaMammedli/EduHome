namespace EduHome.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email, string link, string emailTitle, string subject, string body);
    }
}
