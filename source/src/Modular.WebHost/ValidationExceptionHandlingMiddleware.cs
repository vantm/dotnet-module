using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Modular.WebHost;

internal static class ValidationExceptionHandlingMiddleware
{
    public static async Task Handle(HttpContext context, ILogger logger)
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is ValidationException err)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in err.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Catch {number} validation errors on {url}", err.Errors.Count(), context.Request.Path);
            }

            var errors = modelState.ToDictionary(
                x => x.Key,
                x => x.Value!.Errors.Select(y => y.ErrorMessage).ToArray());

            var result = new HttpValidationProblemDetails(errors);

            context.Response.ContentType = "application/json+problem";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsJsonAsync(result, cancellationToken: context.RequestAborted);
        }
    }
}
