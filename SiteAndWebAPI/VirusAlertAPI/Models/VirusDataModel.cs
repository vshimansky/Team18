using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirusAlertAPI.Models
{
    public class VirusDataModel
    {
        public int Infected { get; set; }
        public int Health { get; set; }
        public int Dead { get; set; }
    }
}