using System;
using System.Globalization;
using System.Net.Mail;
using UniversalAcceptanceLibrary.Exceptions;

namespace UniversalAcceptanceLibrary
{
    public class EmailFormatter: IEmailFormatter
    {
        public string UnicodeToPunycode(string domain)
        {
            try
            {
                var mapping = new IdnMapping();
                return mapping.GetAscii(domain);
            }
            catch (ArgumentException e)
            {
                throw new InvalidUnicodeDomainException(e.Message);
            }
        }

        public string PunycodeToUnicode(string domain)
        {
            try
            {
                var mapping = new IdnMapping();
                return mapping.GetUnicode(domain);
            }
            catch (ArgumentException e)
            {
                throw new InvalidPunycodeDomainException(e.Message);
            }
        }

        public string NormalizeEmail(string email)
        {
            MailAddress mailAddress = null;
            try
            {
                mailAddress = new MailAddress(email);
            }
            catch (FormatException e)
            {
                throw new InvalidEmailException(e.Message);
            }

            var localPart = mailAddress.User;
            var host = mailAddress.Host;

            return string.Concat(localPart, "@", PunycodeToUnicode(UnicodeToPunycode(host)));
        }
    }
}
