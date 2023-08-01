using System.Net;
using CocktailApp.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace CocktailApp.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if(contextFeature != null)
                {
                    var logger = app.ApplicationServices.GetService<ILogger>();
                    logger?.LogError("Something went wrong: {ContextFeatureError}", contextFeature.Error);
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    }.ToString());
                }
            });
        });
    }
}