namespace finWise.Interfaces
{
    public interface IEmailSenderInterface
    {
        void SendEmail(string toEmail, string subject, string userId);
    }
}
