using System;
using System.Net;
using System.Net.Mail;
using UserService.Interfaces;

namespace UserService.Utils
{
    public static class EmailSender
    {
        public static void SendEmail(string email,ISecretTokenEngine secretTokenEngine)
        {
            var from = new MailAddress("traidplatform@mail.ru", "Trading Station");
            var to = new MailAddress(email);
            string secretToken =  secretTokenEngine.GetToken(email).ToString();
            string link = $"https://localhost:5011/users/confirm?secretToken={secretToken}";
            string htmlCode = $"<p>Please, click this link <a href ={link}>link</a>to confirm regestration.</p>";
            var m = new MailMessage(from, to);
            m.Subject = "Registration confirmation";
            m.Body = htmlCode;
            m.IsBodyHtml = true;
            var smtp = new SmtpClient("smtp.mail.ru", 25);
            smtp.Credentials = new NetworkCredential("traidplatform@mail.ru", "t123plat");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
