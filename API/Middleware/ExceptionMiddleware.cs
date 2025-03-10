using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;
        private IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware>logger,IHostEnvironment environment)
        {
            _next= next;
            _logger= logger;
            _environment= environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var res = _environment.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(res, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
