using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTracker.ApplicationServices.Common.Exceptions;
using VehicleTracker.Common.Model;

namespace VehicleTrackerAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IWebHostEnvironment _environment;
        private ILogger<ErrorHandlingMiddleware> Logger { get; set; }
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            this.Logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    string[] errorArray = validationException.Failures.SelectMany(x => x.Value).ToArray();
                    var error = string.Join(";", errorArray);
                    result = JsonConvert.SerializeObject(Result.Fail(error));
                    this.Logger.LogInformation($"An error occured.......{result}");
                    this.Logger.LogInformation("==================================================================================");
                    break;
                case AuthenticationException validationException:
                    code = HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(Result.Fail(string.IsNullOrEmpty(validationException.Message) ? "User is unauthorized." : validationException.Message));
                    this.Logger.LogInformation(result);
                    this.Logger.LogInformation("==================================================================================");
                    break;
                case InvalidDomainException invalidDomainException:
                    code = HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(Result.Fail(string.IsNullOrEmpty(invalidDomainException.Message) ? "omain is invalid" : invalidDomainException.Message));
                    this.Logger.LogInformation(result);
                    this.Logger.LogInformation("==================================================================================");
                    break;
                case PersistenceException persistenceException:
                    code = HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(Result.Fail(string.IsNullOrEmpty(persistenceException.Message) ? "Unable to persist record" : persistenceException.Message));
                    this.Logger.LogInformation(result);
                    this.Logger.LogInformation("==================================================================================");
                    break;
                case DuplicateException duplicateException:
                    code = HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    result = JsonConvert.SerializeObject(Result.Fail(string.IsNullOrEmpty(duplicateException.Message) ? "An error occured on the payload" : duplicateException.Message));
                    this.Logger.LogInformation(result);
                    this.Logger.LogInformation("==================================================================================");
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(
                                     Result.Fail("An error occured. Please contact seven peaks support"));

                this.Logger.LogInformation($"An error occured.......{JsonConvert.SerializeObject(exception, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
                this.Logger.LogInformation("==================================================================================");
            }
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
