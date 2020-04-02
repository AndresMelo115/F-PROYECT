//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MailKit;
//using MimeKit;

//namespace ELECCIONES.Email
//{
//    public class EmailSenderGmail : IEmailSender
//    {
//        private readonly EmailConfiguration _emailConfiguration;

//        public EmailSenderGmail(EmailConfiguration emailConfiguration)
//        {
//            _emailConfiguration = emailConfiguration;
//        }

//        public async Task SendEmailAsync(Message message)
//        {
//            var emailMessage = CreateEmailMessage(message);
//        }

//        private MimeMessage CreateEmailMessage(Message message)
//        {
//            var emailMessage = new MimeMessage();
//            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
//            emailMessage.To.AddRange(message.To);
//            emailMessage.Subject = message.Subject;
//            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
//            { Text = message.Content};


//            return emailMessage;
//        }
        

//    }
//}
