# EventBroker

.NET provides events to notify classes about changes or behavioral occurrences.
To be notified requires knowledge of the class that raises an event. The listener
has to register itself at the class that provides the event.

The concept of an event broker is to not having to know the class that raises
an event. The listener gets registered itself for a specific event at an infrastructure.
This way we can reach a loose coupling over features by just having to know the
events argument which can be defined in an interface assembly.

## How-To

### Define the event arguments

Create a record that contains the relevent data:

```
public record ValueChangedEvent(int value);
```

### Raise the event

Use the IEventBroker interface to raise the event:

```
internal class ValueChangedNotifier
{
  private readonly IEventBroker eventBroker;

  public ValueChangedNotifier(IEventBroker eventBroker)
  {
      this.eventBroker = eventBroker;
  }

  public void NotifyValueChanged(int value)
  {
      this.eventBroker.Publish(new ValueChangedEvent(value));
  }
}
```

### Receiving the event

The class that should get notified needs to implement one of the interfaces
IEventSubscription<T> or IEventSubscriptionAsync<T>.

```
internal class ValueObserver : IEventSubscription<ValueChangedEvent>
{
  public void Handle(ValueChangedEvent data)
  {
      ...
  }
}

internal class ValueObserver : IEventSubscriptionAsync<ValueChangedEvent>
{
  public async Task HandleAsync(ValueChangedEvent data)
  {
      ...
  }
}
```

The observer class needs to be registered using the IEventRegistration:

```
eventRegistration.Register((IEventSubscriptionBase)valueObserver);
```

Best to do this is using the .RegisterOnEventBroker() extension method provided
with the EventBroker.Autofac package. It allows to define the registration when
registering the type in its Autofac module:

```
public class ValueObservationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
      builder.RegisterType<OperationModeChangedObserver>()
          .RegisterOnEventBroker();
  }
}
```
