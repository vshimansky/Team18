using System;
using System.Threading.Tasks;

namespace UniversalAcceptanceLibrary
{
    public class EmailValidator: IEmailValidator
    {
        private readonly ITLDChecker tldChecker;

        public EmailValidator(ITLDChecker tldChecker)
        {
            this.tldChecker = tldChecker;
        }

        public async Task<bool> IsValidEmailAsync(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                var splittedHostName = addr.Host.Split('.');
                var tld = splittedHostName[splittedHostName.Length - 1];
                var tldExists = await tldChecker.IsTLDExistsAsync(tld);

                return (addr.Address == email) && tldExists;
            }
            catch
            {
                return false;
            }
        }
    }
}
