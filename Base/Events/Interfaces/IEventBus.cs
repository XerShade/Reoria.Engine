namespace Reoria.Engine.Base.Events.Interfaces;

/// <summary>
/// Defines the contract for an event bus that handles the subscription and emission of events.
/// It allows clients to connect handlers to specific event types and emit events to trigger those handlers.
/// </summary>
public interface IEventBus : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Connects an event handler to an event type.
    /// The handler will be invoked when the event is emitted.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to connect the handler to.</typeparam>
    /// <param name="handler">The event handler that will be invoked when the event is emitted.</param>
    void Connect<TEvent>(Action<TEvent> handler);

    /// <summary>
    /// Disconnects an event handler from an event type.
    /// The handler will no longer be invoked when the event is emitted.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to disconnect the handler from.</typeparam>
    /// <param name="handler">The event handler to remove.</param>
    void Disconnect<TEvent>(Action<TEvent> handler);

    /// <summary>
    /// Emits an event of type <typeparamref name="TEvent"/>.
    /// All connected handlers for this event type will be invoked.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to emit.</typeparam>
    /// <param name="event">The event instance to pass to the handlers.</param>
    void Emit<TEvent>(TEvent @event);
}
