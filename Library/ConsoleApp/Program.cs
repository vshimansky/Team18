using System;
using System.Threading.Tasks;
using UniversalAcceptanceLibrary;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tldChecker = new TLDChecker();

            var result = await tldChecker.IsTLDExistsAsync("com");
            Console.WriteLine(result);

            result = await tldChecker.IsTLDExistsAsync("рф");
            Console.WriteLine(result);
        }

        static void TestEmailFormatter()
        {
            var en = new EmailFormatter();

            Console.WriteLine(en.UnicodeToPunycode("с-балалайкой.рф"));
            Console.WriteLine(en.NormalizeEmail("медведь@с-балалайкой.рф"));
        }
    }
}
