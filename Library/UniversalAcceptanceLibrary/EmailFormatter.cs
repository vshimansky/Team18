using System;
using System.Globalization;
using System.Net.Mail;

namespace UniversalAcceptanceLibrary
{
    public class EmailFormatter
    {
        public string UnicodeToPunycode(string domain)
        {
            var mapping = new IdnMapping();
            return mapping.GetAscii(domain);
        }

        public string PunycodeToUnicode(string domain)
        {
            var mapping = new IdnMapping();
            return mapping.GetUnicode(domain);
        }

        /// <summary>
        /// Converts an email address to a format suitable for storage and display. 
        /// 1) An address containing only ASCII characters will remain unchanged. 
        /// 2) The address containing the Unicode characters on the right will remain unchanged. 
        /// 3) The address containing Punycode on the right will be converted to an address with Unicode characters on the right.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string NormalizeEmail(string email)
        {
            var ma = new MailAddress(email);
            var localPart = ma.User;
            var host = ma.Host;

            return string.Concat(localPart, "@", PunycodeToUnicode(UnicodeToPunycode(host)));
        }
    }
}
