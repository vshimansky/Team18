using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirusAlertAPI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Uri { get; set; }
    }
}