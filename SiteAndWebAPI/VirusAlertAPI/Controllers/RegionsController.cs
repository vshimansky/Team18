using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VirusAlertAPI.Controllers
{
    public class RegionsController : ApiController
    {
        // GET api/regions/
        public List<DB.Regions> Get()
        {
            var db = new DB.DataContext();
            return db.regions.ToList();
        }
    }
}
