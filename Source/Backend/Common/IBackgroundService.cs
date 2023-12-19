namespace Common;

using System.Threading;
using System.Threading.Tasks;

public interface IBackgroundService
{
    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}