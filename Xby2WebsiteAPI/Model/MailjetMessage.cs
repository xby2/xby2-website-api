using Microsoft.Extensions.Configuration;
using System;

namespace Xby2WebsiteAPI.Model
{
    public class MailjetMessage
    {
        public MailjetEmailUser From { get; private set; }
        public MailjetEmailUser[] To { get; private set; }
        public string Subject{ get; private set; }
        public string TextPart { get; private set; }

        public MailjetMessage(MailjetEmailUser from, MailjetEmailUser[] to, string subject, string textPart)
        {
            From = from;
            To = to;
            Subject = subject;
            TextPart = textPart;
        }

        public static MailjetMessage FromXby2Message(Xby2Message xby2Message, string fromEmail, string toEmail, string subject)
        {
            return new MailjetMessage(
                new MailjetEmailUser(fromEmail),
                new MailjetEmailUser[] { new MailjetEmailUser(toEmail) },
                subject,
                ConstructMessage(xby2Message)
            );
        }

        private static string ConstructMessage(Xby2Message xby2Message)
        {
            return
                "First Name: " + xby2Message.FirstName + "\n" +
                "Last Name: " + xby2Message.LastName + "\n" +
                "Email: " + xby2Message.Email + "\n" +
                "Phone: " + xby2Message.PhoneNumber + "\n" +
                "Message: " + xby2Message.Message;
        }
    }
}
