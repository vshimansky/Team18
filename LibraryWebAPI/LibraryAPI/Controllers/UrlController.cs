using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        [HttpGet]
        public string Normalize(string url)
        {
            return "";
        }

        [HttpPost]
        public string NormalizeP(string url)
        {
            return "";
        }
    }
}