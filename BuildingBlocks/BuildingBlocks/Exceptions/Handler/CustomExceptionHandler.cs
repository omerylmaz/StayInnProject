using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;
public class CustomExceptionHandler
    (ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        // Seq veya diğer loglama sistemine ayrıntılı loglama
        logger.LogError(exception,
            "An error occurred at {time}. Message: {message}. StackTrace: {stackTrace}",
            DateTime.UtcNow,
            exception.Message,
            exception.StackTrace);

        // İstemciye gösterilecek genel hata ayrıntıları
        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException =>
            (
                "An unexpected error occurred. Please try again later.",
                "Internal Server Error",
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),
            ValidationException =>
            (
                "One or more validation errors occurred.",
                "Validation Error",
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            BadRequestException =>
            (
                "The request is invalid.",
                "Bad Request",
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            NotFoundException =>
            (
                "The requested resource was not found.",
                "Not Found",
                context.Response.StatusCode = StatusCodes.Status404NotFound
            ),
            _ =>
            (
                "An unexpected error occurred. Please try again later.",
                "Internal Server Error",
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        // İstemciye gönderilecek ProblemDetails nesnesi
        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path
        };

        // Ek güvenli bilgiler
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        // ValidationException özel durumu için ek bilgiler
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        // İstemciye problem detaylarını gönder
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }


}
