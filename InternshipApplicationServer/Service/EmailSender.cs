using InternshipApplicationServer.ServiceInterface;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Utils;

namespace InternshipApplicationServer.Service
{
    public class EmailSender : EmailInterface
    {
        public void Send(Email emailData)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("EasyInternships@outlook.com"));
            email.To.Add(MailboxAddress.Parse(emailData.to));
            email.Subject = emailData.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailData.text };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("EasyInternships@outlook.com", "moasdsafa123131@!#!%!d");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
