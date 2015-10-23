using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Infrastructure.Mail
{
    public class MailHelper
    {
        public static string SystemName { get; set; }
        public static string SystemEmail { get; set; }
        public static string SiteName { get; set; }
        public static string SiteSupportEmail { get; set; }

        public static bool Send(string mailTo, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Subject = subject;
                mail.To.Add(mailTo);
                mail.From = new MailAddress("mail", "user");

                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "server";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Credentials = new System.Net.NetworkCredential
                     ("mail", "pass");
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex) { return false; }
        }

        public static bool Send(string mailTo, string subject, string message, string email, string password)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Subject = subject;
                mail.To.Add(mailTo);
                mail.From = new MailAddress(email, "Name");

                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "server";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Credentials = new System.Net.NetworkCredential
                     (email, password);
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex) { return false; }
        }

        public static bool AnswerQuestion(string mailTo, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Subject = subject;
                mail.To.Add(mailTo);
                mail.From = new MailAddress("mail", "user");

                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.cukmostar.org";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Credentials = new System.Net.NetworkCredential
                     ("mail", "password");
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
