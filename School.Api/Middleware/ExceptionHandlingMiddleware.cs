using School.Api.Models;
using School.Application.Exceptions;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

                    DbUpdateException dbEx when
                        dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                            => StatusCodes.Status409Conflict,

                    _ => StatusCodes.Status500InternalServerError
                };

                var message = ex switch
                {
                    DbUpdateException dbEx when
                        dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                            => "A record with the same unique value already exists.",

                    _ => ex.Message
                };

                var response = new ErrorResponse
                {
                    Success = false,
                    StatusCode = context.Response.StatusCode,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
