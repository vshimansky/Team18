using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VirusAlertAPI.Controllers
{
    public class VirusDataController : ApiController
    {
        // GET api/virusdata/
        public List<DB.VirusData> Get(int region)
        {
            var db = new DB.DataContext();
            return db.virusData.Where(x => x.vdRegId == region).ToList();
        }
    }
}
