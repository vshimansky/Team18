using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAcceptanceLibrary
{
    public class UrlFormatter : IUrlFormatter
    {
        private readonly IEmailFormatter emailFormatter;

        public UrlFormatter(IEmailFormatter emailFormatter)
        {
            this.emailFormatter = emailFormatter;
        }

        public string NormalizeUrl(string url)
        {
            Uri uri = new Uri(url);
            var host = uri.Host;
            var normalizedHost = emailFormatter.PunycodeToUnicode(emailFormatter.UnicodeToPunycode(host));

            var uriBuilder = new UriBuilder(url);
            uriBuilder.Host = normalizedHost;
            return uriBuilder.Uri.ToString();
        }
    }
}
