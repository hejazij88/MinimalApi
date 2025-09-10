using System.Diagnostics;

namespace MinimalApi.Middlewares;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        await _next(context);
        sw.Stop();


        _logger.LogInformation("Request {method} {path} responded {statusCode} in {ms}ms",
            context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.ElapsedMilliseconds);


        if (!context.Response.HasStarted)
            context.Response.Headers["X-Response-Time-ms"] = sw.ElapsedMilliseconds.ToString();
    }
}