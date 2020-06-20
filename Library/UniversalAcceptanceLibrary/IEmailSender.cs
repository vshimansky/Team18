using System;
using System.Net.Mail;

namespace UniversalAcceptanceLibrary
{
    /// <summary>
    /// Implements EAI-compatible email sending to SMTP server.
    /// </summary>
    public interface IEmailSender : IDisposable
    {
        void SendEmail(MailMessage message);
    }
}
