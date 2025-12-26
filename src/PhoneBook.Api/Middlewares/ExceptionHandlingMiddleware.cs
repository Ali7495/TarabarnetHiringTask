using System.Net;
using FluentValidation;
using PhoneBook.Domain;

namespace PhoneBook.Api;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException domainEx)
        {
            _logger.LogWarning(domainEx, domainEx.Message);
            await WriteError(context, HttpStatusCode.BadRequest, domainEx.Message);
        }
        catch (ValidationException ValidationEx)
        {
            _logger.LogWarning(ValidationEx, "Validation error");

            var errors = ValidationEx.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            await WriteError(context, HttpStatusCode.BadRequest, errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception");
            await WriteError(context, HttpStatusCode.InternalServerError,
                "An error occurred.");
        }

    }


    private static async Task WriteError(HttpContext context, HttpStatusCode httpStatusCode, object message)
    {
        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
            new
            {
                error = message
            }
        );
    }
}
