namespace RaspiRobot.OpenApi.Communication;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Grpc.Core;
using Microsoft.Extensions.Hosting;

internal class GrpcStreamListener
{
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Log logger;

    public GrpcStreamListener(
        IHostApplicationLifetime hostApplicationLifetime,
        Log logger)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.logger = logger;
    }

    public async Task<TResponse[]> ListenAsync<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        Func<TRequest, Task<TResponse>> handler,
        string peer,
        string method,
        CancellationToken cancellationToken)
    {
        this.logger.Debug("{Method} waiting for state update...", method);
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        using CancellationTokenSource taskCompletedSource = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationTokenSource.Token);

        List<Task<TResponse>> requests = [];
        try
        {
            await foreach (TRequest request in requestStream.ReadAllAsync(taskCompletedSource.Token))
            {
                this.logger.Info("{Method} received new request '{Request}'", method, request!);
                requests.Add(
                    Task.Run(
                        async () =>
                        {
                            TResponse response = await handler(request);
                            await taskCompletedSource.CancelAsync();
                            return response;
                        },
                        cancellationTokenSource.Token));
            }
        }
        catch (OperationCanceledException) when (taskCompletedSource.IsCancellationRequested)
        {
            // ignore expected cancellation
        }
        catch (OperationCanceledException ex)
        {
            this.logger.Info(
                ex,
                "Client '{Peer}' has been disconnected within method '{Method}'",
                peer,
                method);
        }

        return await Task.WhenAll(requests);
    }
}