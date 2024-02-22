using System.Globalization;

namespace TypingMaster.Middlewares;

using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

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
