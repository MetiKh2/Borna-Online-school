using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Common.Senders
{
   public class EmailSender
    {
        public static void Send(string To, string Subject, string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mahdikhodarahimi0@gmail.com"," Meti Kh");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("mahdikhodarahimi0@gmail.com", "12212332");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}
