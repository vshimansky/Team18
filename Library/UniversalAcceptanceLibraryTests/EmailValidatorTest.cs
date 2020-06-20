using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UniversalAcceptanceLibrary;

namespace UniversalAcceptanceLibraryTests
{
    [TestClass]
    public class EmailValidatorTest
    {
        [TestMethod]
        public void TestValidAsciiAddress()
        {
            var emailValidator = new EmailValidator();
            string[] addresses = {
                // Tests from Wikipedia
                "simple@example.com",
                "very.common@example.com",
                "disposable.style.email.with+symbol@example.com",
                "other.email-with-hyphen@example.com",
                "fully-qualified-domain@example.com",
                "user.name+tag+sorting@example.com", // (may go to user.name@example.com inbox depending on mail server)
                "x@example.com", // (one-letter local-part)
                "example-indeed@strange-example.com",
                "admin@mailserver1", // (local domain name with no TLD, although ICANN highly discourages dotless email addresses[13])
                "example@s.example", // (see the List of Internet top-level domains)
                "\" \"@example.org", // (space between the quotes)
                "\"john..doe\"@example.org", // (quoted double dot)
                "mailhost!username@example.org", // (bangified host route used for uucp mailers)
                "user%example.com@example.org", // (% escaped mail route to user@example.com via example.org)
             };

            foreach (var address in addresses)
            {
                bool result = emailValidator.IsValidEmail(address);
                Assert.IsTrue(result,
                       String.Format("Expected for '{0}': true; Actual: {1}",
                                     address, result));
            }
        }

        [TestMethod]
        public void TestInvalidAsciiAddress()
        {
            var emailValidator = new EmailValidator();
            string[] addresses = {
                "Abc.example.com", // (no @ character)
                "A@b@c@example.com", // (only one @ is allowed outside quotation marks)
                "a\"b(c)d,e:f;g<h>i[j\\k]l@example.com", // (none of the special characters in this local-part are allowed outside quotation marks)
                "just\"not\"right@example.com", // (quoted strings must be dot separated or the only element making up the local-part)
                "this is\"not\allowed@example.com", // (spaces, quotes, and backslashes may only exist when within quoted strings and preceded by a backslash)
                "this\\ still\"not\\allowed@example.com", // (even if escaped (preceded by a backslash), spaces, quotes, and backslashes must still be contained by quotes)
                // This test fails. See https://tools.ietf.org/html/rfc5321#section-4.5.3.
                // "1234567890123456789012345678901234567890123456789012345678901234+x@example.com", // (local part is longer than 64 characters)
                "i_like_underscore@but_its_not_allow_in _this_part.example.com", // (Underscore is not allowed in domain part)
             };

            foreach (var address in addresses)
            {
                bool result = emailValidator.IsValidEmail(address);
                Assert.IsFalse(result,
                       String.Format("Expected for '{0}': false; Actual: {1}",
                                     address, result));
            }
        }

        [TestMethod]
        public void TestValidEAIAddress()
        {
            var emailValidator = new EmailValidator();
            string[] addresses = {
                "Pelé@example.com", // Latin alphabet with diacritics
                "δοκιμή@παράδειγμα.δοκιμή", // Greek alphabet
                "我買@屋企.香港", //  Traditional Chinese characters
                "二ノ宮@黒川.日本", // Japanese characters
                "медведь@с-балалайкой.рф", // Cyrillic characters
                "संपर्क@डाटामेल.भारत", // Devanagari characters
             };

            foreach (var address in addresses)
            {
                bool result = emailValidator.IsValidEmail(address);
                Assert.IsTrue(result,
                       String.Format("Expected for '{0}': true; Actual: {1}",
                                     address, result));
            }
        }
    }
}
