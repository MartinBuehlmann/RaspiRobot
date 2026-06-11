namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

public class ChangeSubscriber
{
    private const int ChannelCapacity = 1;

    public async Task RunAsync(
        Action<Action> subscribe,
        Action<Action> unsubscribe,
        Func<Task> onChanged,
        CancellationToken cancellationToken)
    {
        var channel = Channel.CreateBounded<object?>(
            new BoundedChannelOptions(ChannelCapacity)
            {
                FullMode = BoundedChannelFullMode.DropWrite,
                SingleReader = true,
                SingleWriter = false,
            });

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

        return;

        void HandleChanged()
            => channel.Writer.TryWrite(null);
    }
}