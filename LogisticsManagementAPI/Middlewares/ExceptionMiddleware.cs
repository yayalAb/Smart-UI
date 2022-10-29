using Application.Common.Exceptions;
using WebApi.Models;
using System.Net;
using System.Security.Authentication;

namespace WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                NotFoundException => (int)HttpStatusCode.NotFound,
                AuthenticationException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            var message = exception.Message;
            ValidationException? ex;
            if ( exception is ValidationException)
            {
                ex = exception as ValidationException;

                if (ex != null)
                {
                    var values = ex.Errors?.Values?.First();

                    values = values == null ? Array.Empty<string>() : values;
                    var errors = String.Join(" , ", values);
                    message = ex.Message + "\n" + errors;
                }

            }

            await context.Response.WriteAsync(new ErrorDetail()
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            }.ToString());
        }
    }
}