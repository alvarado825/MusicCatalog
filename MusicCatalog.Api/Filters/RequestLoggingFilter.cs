using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicCatalog.Api.Filters
{
    public class RequestLoggingFilter : IActionFilter
    {
        private readonly ILogger<RequestLoggingFilter> _logger;

        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var stopwatch = (Stopwatch)context.HttpContext.Items["RequestStopwatch"];
            stopwatch.Stop();

            _logger.LogInformation(
            "Endpoint {Path} executado em {Elapsed} ms",
            context.HttpContext.Request.Path,
            stopwatch.ElapsedMilliseconds);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["RequestStopwatch"] = Stopwatch.StartNew();
        }
    }
}