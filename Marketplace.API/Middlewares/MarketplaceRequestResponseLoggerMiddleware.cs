using Marketplace.Domain.Entities;
using Marketplace.Services;

namespace Marketplace.Middlewares;

public class MarketplaceRequestResponseLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public MarketplaceRequestResponseLoggerMiddleware(
        RequestDelegate next)
    {
        _next = next;
        // _requestLogService = requestLogService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestLogService = context.RequestServices.GetService<IRequestLogService>();

        // Read Request Body
        context.Request.EnableBuffering();
        await ReadBodyStream(context.Request.Body);
        context.Request.Body.Position = 0;
        
        // Setup Response Body
        var originalBodyStream = context.Response.Body;
        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        
        await _next(context);
        
        memoryStream.Seek(0, SeekOrigin.Begin);
        await new StreamReader(context.Response.Body).ReadToEndAsync();
        // var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        memoryStream.Seek(0, SeekOrigin.Begin);

        // Rewrite Back to Body
        context.Response.Body = originalBodyStream;
        await context.Response.Body.WriteAsync(memoryStream.ToArray());
        RequestLog requestLog = new RequestLog()
        {
            StatusCode = context.Response.StatusCode
        };
        await requestLogService.Log(requestLog);
    }

    private async Task<string> ReadBodyStream(Stream body)
    { 
        return await new StreamReader(body).ReadToEndAsync();
    }
}

public static class MarketplaceRequestResponseLoggerMiddlewareExtensions
{
    public static IApplicationBuilder UseMarketplaceRequestResponseLogger(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MarketplaceRequestResponseLoggerMiddleware>();
    }
}