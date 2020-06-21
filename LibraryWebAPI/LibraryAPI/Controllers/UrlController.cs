using Microsoft.AspNetCore.Mvc;
using UniversalAcceptanceLibrary;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        [HttpGet]
        public string Normalize([FromServices] IUrlFormatter urlFormatter, string url)
        {
            var result = urlFormatter.NormalizeUrl(url);
            return result;
        }
    }
}