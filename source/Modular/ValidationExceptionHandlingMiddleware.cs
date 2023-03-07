using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Net;
using System;

namespace Modular;

internal static class ValidationExceptionHandlingMiddleware
{
    public static async Task Handle<TValidationException>(
        HttpContext context,
        Func<TValidationException, IEnumerable<(string PropName, string ErrorMessage)>> getErrors) where TValidationException : Exception
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is TValidationException err)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in getErrors(err))
            {
                modelState.AddModelError(error.PropName, error.ErrorMessage);
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
