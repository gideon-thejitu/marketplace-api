using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace marketplace_api.Middlewares;

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
    // private readonly ILogger<MarketplaceGlobalExceptionHandler> _logger;
    //
    // public MarketplaceGlobalExceptionHandler(ILogger<MarketplaceGlobalExceptionHandler> logger)
    // {
    //     _logger = logger;
    // }
    // public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    // {
    //     try
    //     {
    //         await next(context);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         _logger.LogError(e.Message);
    //         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    //         context.Response.ContentType = MediaTypeNames.Application.Json;
    //         var res = JsonSerializer.Serialize(new
    //         {
    //             Error = e.Message
    //         });
    //
    //         await context.Response.WriteAsync(res); 
    //     }
    // }
}