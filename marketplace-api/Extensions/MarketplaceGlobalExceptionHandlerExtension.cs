using System.Net.Mime;
using marketplace_api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace marketplace_api.Extensions;

public static class MarketplaceGlobalExceptionHandlerExtension 
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                var errorDetails = context.Features.Get<IExceptionHandlerFeature>();
                if (errorDetails is not null)
                {
                    var exception = errorDetails.Error;
                    await context.Response.WriteAsync(exception.ToString());
                }
            });
        });
    }
}