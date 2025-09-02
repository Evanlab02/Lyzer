using Lyzer.Errors;

using Newtonsoft.Json;

namespace Lyzer.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (GeneralException ex)
            {
                _logger.LogError(ex, "Custom HTTP exception occurred.");
                await HandleExceptionAsync(context, ex.StatusCode, ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.", ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                Message = message,
                Details = exception.StackTrace
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}