using Application.Common.Exceptions;
using System.Net;
using System.Security.Authentication;
using SmartUIAPI.Models;

namespace SmartUIAPI.Middlewares
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
                InvalidLoginException => (int)HttpStatusCode.BadRequest,
                AuthenticationException => (int)HttpStatusCode.Unauthorized,
                ForbiddenAccessException => (int)HttpStatusCode.Unauthorized,
                CantCreateUserException => (int)HttpStatusCode.BadRequest,
                PasswordResetException => (int)HttpStatusCode.BadRequest,
                CustomBadRequestException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            var message = exception.Message;


            switch (exception)
            {
                case ValidationException:
                    ValidationException? ex;
                    ex = exception as ValidationException;
                    if (ex != null)
                    {
                        var values = ex.Errors?.Values?.First();

                        values = values == null ? Array.Empty<string>() : values;
                        var errors = String.Join(" , ", values);
                        message = ex.Message + "\n" + errors;

                    }
                    break;
                case CantCreateUserException:
                    CantCreateUserException? ex2;
                    ex2 = exception as CantCreateUserException;
                    if (ex2 != null)
                    {
                        var errors = String.Join(" , ", ex2.Errors);
                        message = ex2.Message + "\n" + errors;
                    }
                    break;

            }

            await context.Response.WriteAsync(new ErrorDetail()
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            }.ToString());
        }
    }
}