namespace RaspiRobot.Logging;

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

    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        long correlationId = Interlocked.Increment(ref currentCorrelationId);

        this.log.LogDebug(
            "[{CorrelationId}] Method {FullName} called with parameter ({TypeName}: {SerializedMessage})",
            correlationId,
            context.Method,
            request.GetType().Name,
            JsonSerializer.Serialize(request));
        Task<TResponse> response = base.UnaryServerHandler(request, context, continuation);
        this.log.LogDebug(
            "[{CorrelationId}] Method {FullName} returned ({TypeName}: {SerializedMessage})",
            correlationId,
            context.Method,
            response.GetType().Name,
            this.SerializeMessage(response));

        return response;
    }

    public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> request,
        ServerCallContext context,
        ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        long correlationId = Interlocked.Increment(ref currentCorrelationId);

        this.log.LogDebug(
            "[{CorrelationId}] Async Method {FullName} called with parameter ({TypeName}: {SerializedMessage})",
            correlationId,
            context.Method,
            typeof(TResponse).Name,
            JsonSerializer.Serialize(request));

        return base.ClientStreamingServerHandler(request, context, continuation);
    }

    public override Task ServerStreamingServerHandler<TRequest, TResponse>(
        TRequest request,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        long correlationId = Interlocked.Increment(ref currentCorrelationId);
        this.log.LogDebug(
            "[{CorrelationId}] Opening server stream of method {FullName}",
            correlationId,
            context.Method);

        return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
    }

    public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        long correlationId = Interlocked.Increment(ref currentCorrelationId);
        this.log.LogDebug(
            "[{CorrelationId}] Opening duplex server stream of method {FullName}",
            correlationId,
            context.Method);

        return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
    }

    private string SerializeMessage<TResponse>(TResponse response)
        => JsonSerializer.Serialize(response, this.jsonSerializerOptions);
}