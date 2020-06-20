using System.Net.Mail;

namespace UniversalAcceptanceLibrary
{
    public class EmailSender: IEmailSender
    {
        private readonly SmtpClient smtpClient;

        public EmailSender(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
            smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;
        }

        public void SendEmail(MailMessage message)
        {
            smtpClient.Send(message);
        }

        public void Dispose()
        {
            smtpClient.Dispose();
        }
    }
}
