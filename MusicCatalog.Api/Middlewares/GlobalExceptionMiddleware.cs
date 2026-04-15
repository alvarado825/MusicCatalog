using System.Diagnostics;
using MusicCatalog.Api.Extensions;

namespace MusicCatalog.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;


        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {   
                string traceId = Activity.Current?.Id ?? context.TraceIdentifier;

                _logger.LogError(ex, "Erro não tratado na requisição. TraceId: {TraceId}", traceId);

                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string traceId)
        {
            var statusCode = GlobalExceptionMiddlewareExtensions.GetStatusCode(exception);

            var problem = GlobalExceptionMiddlewareExtensions.CreateErrorDetail(exception, _environment, statusCode, traceId);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}