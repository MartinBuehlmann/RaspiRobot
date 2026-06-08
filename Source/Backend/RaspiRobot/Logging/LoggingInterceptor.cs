namespace RaspiRobot.Logging;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

public class LoggingInterceptor : Interceptor
{
    private static long currentCorrelationId;
    private readonly JsonSerializerOptions jsonSerializerOptions;

    private readonly ILogger<LoggingInterceptor> log;

    public LoggingInterceptor(ILogger<LoggingInterceptor> log)
    {
        this.log = log;

        this.jsonSerializerOptions = new JsonSerializerOptions();
        this.jsonSerializerOptions.Converters.Add(new ByteStringConverter());
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        using IDisposable? scope = this.BeginLoggingScope(context);
        this.log.LogDebug(
            "Method called with parameter ({TypeName}: {SerializedMessage})",
            typeof(TRequest).Name,
            this.SerializeMessage(request));

        try
        {
            TResponse response = await base.UnaryServerHandler(request, context, continuation);

            this.log.LogDebug(
                "Method returned ({TypeName}: {SerializedMessage})",
                typeof(TResponse).Name,
                this.SerializeMessage(response));

            return response;
        }
        catch (Exception exception)
        {
            this.log.LogError(exception, "Method threw an exception");
            throw;
        }
    }

    public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> request,
        ServerCallContext context,
        ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        using IDisposable? scope = this.BeginLoggingScope(context);
        this.log.LogDebug("Client streaming method called");

        try
        {
            TResponse response = await base.ClientStreamingServerHandler(request, context, continuation);

            this.log.LogDebug(
                "Client streaming method returned ({TypeName}: {SerializedMessage})",
                typeof(TResponse).Name,
                this.SerializeMessage(response));

            return response;
        }
        catch (Exception exception)
        {
            this.log.LogError(exception, "Client streaming method threw an exception");
            throw;
        }
    }

    public override Task ServerStreamingServerHandler<TRequest, TResponse>(
        TRequest request,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        using IDisposable? scope = this.BeginLoggingScope(context);
        this.log.LogDebug(
            "Opening server stream with parameter ({TypeName}: {SerializedMessage})",
            typeof(TRequest).Name,
            this.SerializeMessage(request));

        try
        {
            return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }
        catch (Exception exception)
        {
            this.log.LogError(exception, "Server stream threw an exception");
            throw;
        }
    }

    public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        using IDisposable? scope = this.BeginLoggingScope(context);
        this.log.LogDebug("Opening duplex server stream");

        try
        {
            return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
        }
        catch (Exception exception)
        {
            this.log.LogError(exception, "Duplex server stream threw an exception");
            throw;
        }
    }

    private IDisposable? BeginLoggingScope(ServerCallContext context)
    {
        long correlationId = Interlocked.Increment(ref currentCorrelationId);

        return this.log.BeginScope(
            new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["GrpcMethod"] = context.Method,
                ["Peer"] = context.Peer,
            });
    }

    private string SerializeMessage<TResponse>(TResponse response)
        => JsonSerializer.Serialize(response, this.jsonSerializerOptions);
}