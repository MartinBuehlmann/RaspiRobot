namespace RaspiRobot.BackgroundServices;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RaspiRobot.Common;

internal class BackgroundServiceHost : IHostedService
{
    private readonly IReadOnlyList<IBackgroundService> backgroundServices;

    public BackgroundServiceHost(IEnumerable<IBackgroundService> backgroundServices)
    {
        this.backgroundServices = backgroundServices.ToList();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(this.backgroundServices.Select(x => x.StartAsync(cancellationToken)));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(this.backgroundServices.Select(x => x.StopAsync(cancellationToken)));
    }
}