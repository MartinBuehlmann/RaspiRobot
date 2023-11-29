using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RaspiRobot.Logging;

using System.Net.Mime;
using System.Text;
using Serilog;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    // ReSharper disable once UnusedMember.Global called by framework
    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.Path.HasValue &&
            httpContext.Request.ContentType != null &&
            httpContext.Request.ContentType.Contains(
                MediaTypeNames.Application.Json,
                StringComparison.InvariantCulture))
        {
            httpContext.Request.EnableBuffering();
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, false, leaveOpen: true);
            string payload = await reader.ReadToEndAsync();
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            Log.Information(
                "Request {Method} {Path} with Payload: {Payload}",
                httpContext.Request.Method,
                httpContext.Request.Path.Value,
                !httpContext.Request.Path.Value.Contains(
                    "Login",
                    StringComparison.InvariantCultureIgnoreCase) ? payload : "[login data]");
        }

        await this.next(httpContext);
    }
}