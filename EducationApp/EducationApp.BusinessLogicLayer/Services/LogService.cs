using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{ 
    public class LogService
    {
        private readonly ILogger<LogService> logger;
        private readonly RequestDelegate next;

        public LogService(ILogger<LogService> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items["CorrelationId"] = Guid.NewGuid().ToString();
            logger.LogInformation($"About to start {context.Request.Method} {context.Request.GetDisplayUrl()} request");

            await next(context);

            logger.LogInformation($"Request completed with status code: {context.Response.StatusCode} ");
        }
    }
}