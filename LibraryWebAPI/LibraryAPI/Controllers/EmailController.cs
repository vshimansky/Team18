using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using UniversalAcceptanceLibrary;
using UniversalAcceptanceLibrary.Exceptions;

namespace LibraryAPI.Controllers
{
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
        public async Task<IActionResult> Validate([FromServices] IEmailValidator emailValidator, string email)
        {
            var result = await emailValidator.IsValidEmailAsync(email);

            if (result)
            {
                return Ok(new SuccessResponse { Message = "Email is valid." });
            }

            throw new InvalidEmailException("The specified string is not in the form required for an e-mail address.");
        }

        [HttpGet]
        public ActionResult<string> Normalize([FromServices] IEmailFormatter emailFormatter, string email)
        {
            var result = emailFormatter.NormalizeEmail(email);
            return result;
        }

        [HttpPost]
        public IActionResult Send([FromServices] IEmailSender emailSender, SendEmailInputModel inputModel)
        {
            MailAddress fromAddress = new MailAddress(inputModel.From);
            MailAddress toAddress = new MailAddress(inputModel.To);

            MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = inputModel.Subject,
                Body = inputModel.Body,
                IsBodyHtml = inputModel.IsHtml
            };

            emailSender.SendEmail(message);

            return Ok(new SuccessResponse { Message = "Email successfully sent." });
        }
    }
}