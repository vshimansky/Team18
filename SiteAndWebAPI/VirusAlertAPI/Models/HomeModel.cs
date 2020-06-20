using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirusAlertAPI.Models
{
    public class HomeModel
    {
        public string Email { get; set; }
        public List<NewsModel> News { get; set; }

        public string Map { get; set; }
        public string Diagram { get; set; }

        public VirusDataModel Data { get; set; }
        public List<LimitModel> Limits { get; set; }    
    }
}