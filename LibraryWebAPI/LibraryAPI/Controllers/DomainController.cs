using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversalAcceptanceLibrary;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MethodInfo> List()
        {
            return new List<MethodInfo> {
                new MethodInfo() {MethodName= "unicodetopunycode", MethodDescr= "Преобразование доменного имени из UTF-8 в Punycode" },
                new MethodInfo() {MethodName= "punycodetounicode", MethodDescr= "Преобразование доменного имени из Punycode в UTF-8" },
                new MethodInfo() {MethodName= "checktld", MethodDescr= "Проверка существования TLD" },
            };
        }

        [HttpGet]
        public ActionResult<string> UnicodeToPunycode([FromServices] IEmailFormatter emailFormatter, string domain)
        {
            var result = emailFormatter.UnicodeToPunycode(domain);
            return result;
        }

        [HttpGet]
        public ActionResult<string> PunycodeToUnicode([FromServices] IEmailFormatter emailFormatter, string domain)
        {
            var result = emailFormatter.PunycodeToUnicode(domain);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<string>> CheckTld([FromServices] ITLDChecker tldChecker, string tld)
        {
            var result = await tldChecker.IsTLDExistsAsync(tld);

            if (result)
                return Ok(new SuccessResponse { Message = "TLD exists." });

            return BadRequest();
        }
    }
}