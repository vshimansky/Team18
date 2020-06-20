using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    public struct LinkData
    {
        public int start { get; set; }
        public int end { get; set; }
        public string link { get; set; }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LinkController : ControllerBase
    {

        [HttpGet]
        public LinkData Extract(string source)
        {
            return new LinkData();
        }

        [HttpPost]
        public LinkData ExtractP(string source)
        {
            return new LinkData();
        }
    }
}