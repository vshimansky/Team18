using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UniversalAcceptanceLibrary.Exceptions;

namespace LibraryAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private static readonly Dictionary<Type, HttpStatusCode> exceptionToHttpCode = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(FormatException), HttpStatusCode.UnprocessableEntity },
            { typeof(ArgumentException), HttpStatusCode.UnprocessableEntity },
            { typeof(InvalidEmailException), HttpStatusCode.UnprocessableEntity },
            { typeof(InvalidUnicodeDomainException), HttpStatusCode.UnprocessableEntity },
            { typeof(InvalidPunycodeDomainException), HttpStatusCode.UnprocessableEntity },
        };
        private static readonly Dictionary<Type, int> exceptionToErrorCode = new Dictionary<Type, int>
        {
            { typeof(InvalidEmailException), 1001 },
            { typeof(InvalidUnicodeDomainException), 1011 },
            { typeof(InvalidPunycodeDomainException), 1012 },
        };

        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var httpCode = HttpStatusCode.InternalServerError;
            var errorCode = 0;

            exceptionToErrorCode.TryGetValue(ex.GetType(), out errorCode);
            exceptionToHttpCode.TryGetValue(ex.GetType(), out httpCode);

            var result = JsonConvert.SerializeObject(new { errorDescription = ex.Message, errorCode });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpCode;
            return context.Response.WriteAsync(result);
        }
    }
}
