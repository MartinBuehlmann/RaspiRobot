namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

// TODO: Review and refactor or search for better solution
public class ChangeSubscriber
{
    public async Task RunAsync(
        Action<Action> subscribe,
        Action<Action> unsubscribe,
        Func<Task> onChanged,
        CancellationToken cancellationToken)
    {
        var channel = Channel.CreateBounded<object?>(
            new BoundedChannelOptions(1)
            {
                FullMode = BoundedChannelFullMode.DropWrite,
                SingleReader = true,
                SingleWriter = false,
            });

        void HandleChanged() => channel.Writer.TryWrite(null);

        subscribe(HandleChanged);
        try
        {
            await onChanged();
            await foreach (object? o in channel.Reader.ReadAllAsync(cancellationToken))
            {
                await onChanged();
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        finally
        {
            unsubscribe(HandleChanged);
            channel.Writer.TryComplete();
        }
    }
}