using finWise.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace finWise.Repositories
{
    public class EmailSenderRepository : IEmailSenderInterface
    {
        public void SendEmail(string toEmail, string subject, string userId)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 465);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("jainmahira567@gmail.com", "mlwt sgqr bsaz ppbx");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("jainmahira567@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Dear User, </h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<h2>Welcome to finWise</h2>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>We are delighted to have you as a user of our app. Your registration is successful. To get started, simply log in to your account using your registered email and password.</p>");
            mailBody.AppendFormat("<p>Your UserID is: <strong>{0}</strong></p>", userId);
            mailBody.AppendFormat("<br />");
            mailMessage.Body = mailBody.ToString();
            client.Send(mailMessage);
        }
    }
}
