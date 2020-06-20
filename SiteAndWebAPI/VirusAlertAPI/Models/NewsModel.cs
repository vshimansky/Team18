using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirusAlertAPI.Models
{
    public class NewsModel
    {
        public string Image { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string FullText { get; set; }
    }
}