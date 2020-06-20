using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UniversalAcceptanceLibrary;

namespace UniversalAcceptanceLibraryTests
{
    [TestClass]
    public class EmailFormatterTest
    {
        [TestMethod]
        public void TestNormalizeAcsiiAddress()
        {
            var emailFormatter = new EmailFormatter();

            string[] addresses = {
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
                var result = emailFormatter.NormalizeEmail(address);
                Assert.AreEqual(address, result,
                       String.Format("Expected for '{0}': true; Actual: {1}",
                                     address, result));
            }
        }

        [TestMethod]
        public void TestNormalizeEAIAddressInUnicode()
        {
            var emailFormatter = new EmailFormatter();

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
                var result = emailFormatter.NormalizeEmail(address);
                Assert.AreEqual(address, result,
                       String.Format("Expected for '{0}': true; Actual: {1}",
                                     address, result));
            }
        }

        [TestMethod]
        public void TestNormalizeEAIAddressInPunicode()
        {
            var emailFormatter = new EmailFormatter();

            string[,] addresses = {
                { "медведь@xn----8sbaac5cahfb0b0a.xn--p1ai", "медведь@с-балалайкой.рф"}, // Cyrillic characters
                { "user@xn----8sbaac5cahfb0b0a.xn--p1ai", "user@с-балалайкой.рф"}, // Cyrillic characters, ASCII local part
                { "δοκιμή@xn--hxajbheg2az3al.xn--jxalpdlp", "δοκιμή@παράδειγμα.δοκιμή" }, // Greek alphabet
             };

            for (int i = 0; i < addresses.GetLength(0); i++)
            {
                var result = emailFormatter.NormalizeEmail(addresses[i, 0]);
                Assert.AreEqual(addresses[i, 1], result,
                       String.Format("Expected for '{0}': true; Actual: {1}",
                                     addresses[i, 1], result));
            }
        }
    }
}
