using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MinGlass.Models;
using MinGlass.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MinGlass.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(
            ILogger<ErrorHandlingMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorMessage;
            HttpStatusCode statusCode;

            if (exception is MinglassUnauthorizationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                errorMessage = exception.Message;
            }
            else if (exception is MinglassException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = exception.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = "Something went wrong!";
                _logger.LogError(exception, errorMessage);
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsJsonAsync(new ErrorResponse(errorMessage));
        }
    }
}
