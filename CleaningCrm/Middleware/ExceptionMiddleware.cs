using System.Text.Json;

namespace CleaningCrm.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (KeyNotFoundException exception)
        {
            _logger.LogWarning(exception, "Resource not found");
            await WriteErrorResponse(context, StatusCodes.Status404NotFound, exception.Message);
        }
        catch (UnauthorizedAccessException exception)
        {
            _logger.LogWarning(exception, "Unauthorized access");
            await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            _logger.LogWarning(exception, "Invalid operation");
            await WriteErrorResponse(context, StatusCodes.Status400BadRequest, exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");
            await WriteErrorResponse(context, StatusCodes.Status500InternalServerError, "Внутрішня помилка сервера");
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        object errorResponse = new
        {
            error = message,
            statusCode = statusCode
        };

        string json = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(json);
    }
}