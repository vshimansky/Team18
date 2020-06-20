using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UniversalAcceptanceLibrary;

namespace LibraryAPI.Controllers
{
    public struct ApiResult
    {
        public bool Result { get; set; }
        public string Output { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<MethodInfo> List()
        {
            return new List<MethodInfo> {
                new MethodInfo() {MethodName= "validate", MethodDescr= "Валидация email" },
                new MethodInfo() {MethodName= "normalize", MethodDescr= "Нормализация email для хранения" },
            };
        }

        [HttpGet]
        [HttpPost]
        public ApiResult Validate(string email)
        {
            var emailValidator = new EmailValidator();
            var result = emailValidator.IsValidEmail(email);

            if (result)
            {
                return new ApiResult() { Result = true, Output = "Почтовый адрес " + email  + " корректен" };
            }
            else
            {
                return new ApiResult()
                {
                    Result = false,
                    Output = "Почтовый адрес " + email + " не  корректен"
                };
            }
        }

        [HttpGet]
        [HttpPost]
        public ActionResult<string> Normalize(string email)
        {
            var emailFormatter = new EmailFormatter();
            try
            {
                var result = emailFormatter.NormalizeEmail(email);
                return result;
            } 
            catch (FormatException)
            {
                return BadRequest();
            }
        }

        /*
        [HttpPost]
        public string Send(string from, string to, string subject, string body, bool isHtml)
        {
            return "OK";
        }
        */
    }
}