using System.Globalization;
using System.Diagnostics;

namespace TypingMaster.Middlewares;

public class RequestTimingMiddleware(RequestDelegate next)
{

    private const string HeaderKey = "X-Request-Duration";

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            context.Response.Headers.Add(HeaderKey, stopwatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return Task.CompletedTask;
        });

        await next(context);
    }
}
