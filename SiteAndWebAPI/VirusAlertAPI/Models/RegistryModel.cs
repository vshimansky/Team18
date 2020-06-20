using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirusAlertAPI.Models
{
    public class RegistryModel
    {
        public string Name { get; set; }
        public string Oms { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Error { get; set; }
    }
}