using Microsoft.AspNetCore.Diagnostics;
using System.Net;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Lỗi xảy ra: {Message}", exception.Message);

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Lỗi hệ thống";
        var detail = exception.Message;

        if (exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = "Không có quyền truy cập";
        }
        else if (exception is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = "Tham số không hợp lệ";
        }
        else if (exception is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            message = "Không tìm thấy dữ liệu";
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            statusCode = (int)statusCode,
            message,
            detail
        };

        await context.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
