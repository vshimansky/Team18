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
        [HttpPost]
        public ActionResult<string> UnicodeToPunycode(string domain)
        {
            var emailFormatter = new EmailFormatter();

            try
            {
                var result = emailFormatter.UnicodeToPunycode(domain);
                return result;
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult<string> PunycodeToUnicode(string domain)
        {
            var emailFormatter = new EmailFormatter();

            try
            {
                var result = emailFormatter.PunycodeToUnicode(domain);
                return result;
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<string>> CheckTld(string tld)
        {
            var tldChecker = new TLDChecker();
            var result = await tldChecker.IsTLDExistsAsync(tld);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}