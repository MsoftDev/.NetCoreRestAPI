using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Middleware;

public class RequestTimerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestTimerMiddleware> logger;

    public RequestTimerMiddleware(RequestDelegate next, ILogger<RequestTimerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopWatch = new Stopwatch();
try
{
    stopWatch.Start();
    await next(context);
}
finally
{
    stopWatch.Stop();
    var elapsedMilliseconds = stopWatch.ElapsedMilliseconds;
        logger.LogInformation(
            "{RequestMethod} {RequestPath} request took {EllapsedMilliseconds}ms to complete",
            context.Request.Method,
            context.Request.Path,
            elapsedMilliseconds);
}
    }
}