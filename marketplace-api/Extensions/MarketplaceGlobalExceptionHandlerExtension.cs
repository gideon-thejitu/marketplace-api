using System.Net.Mime;
using System.Text.Json;
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
                // logger.LogError("Something is wrong");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var errorDetails = context.Features.Get<IExceptionHandlerFeature>();
                var errorMessage = "";
                if (errorDetails is not null)
                {
                    var exception = errorDetails.Error;
                    errorMessage = exception.Message;
                }
                else
                {
                    errorMessage = "This is critical";
                }

                // logger.LogError(errorMessage);

                var res = JsonSerializer.Serialize(new
                {
                    Error = errorMessage
                });

                await context.Response.WriteAsync(res);
            });
        });
    }
}