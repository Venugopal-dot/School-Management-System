using School.Api.Models;
using School.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace School.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,

                    BusinessValidationException => StatusCodes.Status400BadRequest,

                    DuplicateRecordException => StatusCodes.Status409Conflict,

                    _ => StatusCodes.Status500InternalServerError
                };

                var response = new ErrorResponse
                {
                    Success = false,
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
