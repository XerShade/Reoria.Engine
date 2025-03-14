using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Container.Services;
using Reoria.Engine.Base.Events.Interfaces;
using System.Collections.Concurrent;

namespace Reoria.Engine.Base.Events;

/// <summary>
/// EventBus is a singleton class that handles the subscription and emission of events.
/// It allows clients to connect handlers to specific event types and emit events to trigger those handlers.
/// </summary>
[Container]
public class EventBus : Disposable, IEventBus
{
    #region Event Bus: Singleton Pattern
    /// <summary>
    /// Gets the single instance of the EventBus. If not yet created, throws a NullReferenceException.
    /// </summary>
    private static IEventBus? instance;
    /// <summary>
    /// Gets the single instance of the EventBus. If not yet created, throws a NullReferenceException.
    /// </summary>
    public static IEventBus Instance => EventBus.instance ?? throw new NullReferenceException("Event bus has not been created by the dependency injection container yet.");
    #endregion

    #region Event Bus: Service Definitions
    /// <summary>
    /// This method is called during the service registration phase, typically by <see cref="IEngineContainer"/> 
    /// when setting up the application's dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="ContainerServiceDefinitions"/> instance used to register services with the container.</param>
    [ContainerAttribute.DiscoverSerivceDefinitions]
    protected static void RegisterServices(ContainerServiceDefinitions services) => services.AddSingleton<IEventBus, EventBus>();
    #endregion

    /// <summary>
    /// A dictionary that holds event handlers, mapped by their event type.
    /// The key is the type of the event, and the value is a list of delegates (handlers) for that event type.
    /// </summary>
    protected readonly ConcurrentDictionary<Type, List<Delegate>> eventHandlers;

    /// <summary>
    /// Logger used to log events, errors, and other relevant information.
    /// </summary>
    protected readonly ILogger<EventBus> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBus"/> class.
    /// Throws an InvalidOperationException if more than one instance is created.
    /// </summary>
    /// <param name="logger">The logger used for logging events and errors.</param>
    public EventBus(ILogger<EventBus> logger)
    {
        // Check to see if an event bus is already created.
        if (EventBus.instance != null)
        {
            // Yes one is, we do not want multiple instance of this class.
            throw new InvalidOperationException("Unable to create multiple instances of the event bus.");
        }

        // Assign class fields and singleton instance.
        this.logger = logger;
        this.eventHandlers = [];
        EventBus.instance = this;
    }

    /// <summary>
    /// Frees all managed objects associated with the EventBus and clears the event handlers dictionary.
    /// </summary>
    protected override void FreeManagedObjects()
    {
        base.FreeManagedObjects();

        lock (this.@lock)
        {
            // Iterates over all event handlers and clears them
            foreach (Type? key in this.eventHandlers.Keys.ToList())
            {
                this.eventHandlers[key].Clear();
                _ = this.eventHandlers.TryRemove(key, out _);
            }
            this.eventHandlers.Clear();
        }
    }

    /// <summary>
    /// Frees all managed objects asynchronously.
    /// </summary>
    protected async override ValueTask FreeManagedObjectsAsync()
    {
        await base.FreeManagedObjectsAsync();
        await Task.Run(this.FreeManagedObjects);
    }

    /// <summary>
    /// Connects an event handler to an event type.
    /// The handler will be called whenever the specified event is emitted.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to connect the handler to.</typeparam>
    /// <param name="handler">The handler that will process the event when emitted.</param>
    public void Connect<TEvent>(Action<TEvent> handler)
    {
        lock (this.@lock)
        {
            ArgumentNullException.ThrowIfNull(handler);

            // If the event type does not have any handlers, create a new list for it
            if (!this.eventHandlers.ContainsKey(typeof(TEvent)))
            {
                this.eventHandlers[typeof(TEvent)] = [];
            }

            // Add the handler to the list of event handlers
            this.eventHandlers[typeof(TEvent)].Add(handler);
        }
    }

    /// <summary>
    /// Disconnects an event handler from an event type.
    /// The handler will no longer be called when the event is emitted.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to disconnect the handler from.</typeparam>
    /// <param name="handler">The handler that should be removed from the event's handlers.</param>
    public void Disconnect<TEvent>(Action<TEvent> handler)
    {
        lock (this.@lock)
        {
            // If the event type has registered handlers
            if (this.eventHandlers.ContainsKey(typeof(TEvent)))
            {
                // Remove the specified handler
                _ = this.eventHandlers[typeof(TEvent)].Remove(handler);

                // If no handlers remain for the event, remove the event type from the handlers dictionary
                if (this.eventHandlers[typeof(TEvent)].Count <= 0)
                {
                    _ = this.eventHandlers.TryRemove(typeof(TEvent), out _);
                }
            }
        }
    }

    /// <summary>
    /// Emits an event of type <typeparamref name="TEvent"/>.
    /// All connected handlers for this event will be invoked.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to emit.</typeparam>
    /// <param name="event">The event instance that will be passed to each handler.</param>
    public void Emit<TEvent>(TEvent @event)
    {
        lock (this.@lock)
        {
            // If there are handlers for this event type
            if (this.eventHandlers.ContainsKey(typeof(TEvent)))
            {
                // Invoke each handler for the event
                foreach (Delegate handler in this.eventHandlers[typeof(TEvent)])
                {
                    ((Action<TEvent>)handler)?.Invoke(@event);
                }
            }
        }
    }

    /// <summary>
    /// Emits an event of type <typeparamref name="TEvent"/>.
    /// All connected handlers for this event will be invoked.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to emit.</typeparam>
    /// <param name="parameters">The parameters to pass to the handlers.</param>
    public void Emit<TEvent>(params object[] parameters)
    {
        lock (this.@lock)
        {
            // If there are handlers for this event type
            if (this.eventHandlers.ContainsKey(typeof(TEvent)))
            {
                // Create an event instance using the parameters provided.
                TEvent? @event = (TEvent?)Activator.CreateInstance(typeof(TEvent), parameters);

                // Verify that an event instance was created.
                if(@event != null)
                {
                    // Invoke each handler for the event
                    foreach (Delegate handler in this.eventHandlers[typeof(TEvent)])
                    {

                        ((Action<TEvent>)handler)?.Invoke(@event);
                    }
                }
            }
        }
    }
}