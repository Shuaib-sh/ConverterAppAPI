using App.Application.Common;
using System.Net;
using System.Text.Json;

namespace ConverterApp.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware( RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;
            List<ApiError> errors = new();

            switch (exception)
            {
                case FluentValidation.ValidationException validationEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Validation Failed";

                    errors = validationEx.Errors
                        .Select(e => new ApiError(e.PropertyName, e.ErrorMessage))
                        .ToList();
                    break;

                case System.Text.Json.JsonException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Invalid JSON Format";

                    errors.Add(new ApiError("INVALID_JSON", exception.Message));
                    break;

                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Invalid Argument";

                    errors.Add(new ApiError("INVALID_ARGUMENT", exception.Message));
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = "Resource Not Found";

                    errors.Add(new ApiError("NOT_FOUND", exception.Message));
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "Internal Server Error";

                    errors.Add(new ApiError("SERVER_ERROR", exception.Message));
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = ApiResponse<string>.FailureResponse(message, errors);

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

    }
}
