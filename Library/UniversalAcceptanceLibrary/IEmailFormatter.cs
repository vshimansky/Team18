namespace UniversalAcceptanceLibrary
{
    public interface IEmailFormatter
    {
        string UnicodeToPunycode(string domain);

        string PunycodeToUnicode(string domain);

        /// <summary>
        /// Converts an email address to a format suitable for storage and display. 
        /// 1) An address containing only ASCII characters will remain unchanged. 
        /// 2) The address containing the Unicode characters on the right will remain unchanged. 
        /// 3) The address containing Punycode on the right will be converted to an address with Unicode characters on the right.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        string NormalizeEmail(string email);
    }
}
