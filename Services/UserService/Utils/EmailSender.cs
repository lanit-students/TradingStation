using System.Net;
using System.Net.Mail;

namespace UserService.Utils
{
    public static class EmailSender
    {
        public static void SendEmail(string email)
        {
            MailAddress from = new MailAddress("traidplatform@mail.ru", "Trading Station");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Registration confirmation";
            m.Body = $"Please, click this link https://localhost:5011/users/confirm?secretToken={email}";
            m.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
            smtp.Credentials = new NetworkCredential("traidplatform@mail.ru", "t123plat");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
