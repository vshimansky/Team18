using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UniversalAcceptanceLibrary
{
    public class TLDChecker: ITLDChecker
    {
        private const string TLD_CHECK_URL = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";

        private static readonly HttpClient client = new HttpClient();
        private static readonly EmailFormatter emailFormatter = new EmailFormatter();

        public async Task<bool> IsTLDExistsAsync(string tld)
        {
            if (string.IsNullOrEmpty(tld))
            {
                return false;
            }

            string tldInPunycode = null;
            try
            {
                tldInPunycode = emailFormatter.UnicodeToPunycode(tld);
            }
            catch (ArgumentException)
            {
                return false;
            }

            var tldApiRespone = await client.GetStringAsync(TLD_CHECK_URL);
            var tldList = tldApiRespone.Split('\n');

            for (var i = 1; i < tldList.Length; i++)
            {
                if (string.IsNullOrEmpty(tldList[i]))
                {
                    continue;
                }

                var tldFromListInPunycode = emailFormatter.UnicodeToPunycode(tldList[i]);
                if (tldFromListInPunycode.Equals(tldInPunycode, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
