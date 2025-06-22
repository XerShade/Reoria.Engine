using Reoria.Engine.Signals.Interfaces;

namespace Reoria.Engine.Signals;

/// <summary>
/// Implements a simple signal bus system for managing named events and their subscribers.
/// Allows clients to connect callbacks to signals, emit signals to invoke callbacks,
/// and disconnect callbacks when no longer needed.
/// </summary>
public partial class SignalBus : ISignalBus
{
    /// <summary>
    /// Stores the registered callbacks for each signal key.
    /// The key is a string identifier for the signal.
    /// The value is a list of delegates (callbacks) associated with that signal.
    /// </summary>
    protected readonly Dictionary<string, List<Delegate>> Signals = [];

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The unique string identifier of the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect(string key, Action callback)
        => this.AddCallback(key, callback);

    /// <summary>
    /// Emits (triggers) the signal identified by <paramref name="key"/>,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <param name="key">The unique string identifier of the signal to emit.</param>
    public virtual void Emit(string key)
        => this.InvokeCallbacks<Action>(key, callback => callback());

    /// <summary>
    /// Disconnects (removes) a previously connected callback from a signal identified by <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The unique string identifier of the signal.</param>
    /// <param name="callback">The callback to remove from the signal's invocation list.</param>
    public virtual void Disconnect(string key, Action callback)
        => this.RemoveCallback(key, callback);

    /// <summary>
    /// Adds a callback delegate to the signal's callback list for the specified <paramref name="key"/>.
    /// If the signal key does not exist, it creates a new list for it.
    /// </summary>
    /// <param name="key">The signal key.</param>
    /// <param name="callback">The callback delegate to add.</param>
    protected virtual void AddCallback(string key, Delegate callback)
    {
        if (!this.Signals.TryGetValue(key, out List<Delegate>? list))
        {
            this.Signals.Add(key, []);
            list = this.Signals[key];
        }
        list.Add(callback);
    }

    /// <summary>
    /// Removes a callback delegate from the signal's callback list.
    /// If the list becomes empty, the signal key is removed from the dictionary.
    /// </summary>
    /// <param name="key">The signal key.</param>
    /// <param name="callback">The callback delegate to remove.</param>
    protected virtual void RemoveCallback(string key, Delegate callback)
    {
        if (this.Signals.TryGetValue(key, out List<Delegate>? list))
        {
            _ = list.Remove(callback);
            if (list.Count == 0)
            {
                _ = this.Signals.Remove(key);
            }
        }
    }

    /// <summary>
    /// Invokes all callbacks of type <typeparamref name="TDelegate"/> associated with the given signal <paramref name="key"/>.
    /// Uses the provided <paramref name="invoker"/> action to invoke each callback safely.
    /// </summary>
    /// <typeparam name="TDelegate">The delegate type expected for the callbacks (e.g., Action).</typeparam>
    /// <param name="key">The signal key.</param>
    /// <param name="invoker">The action that defines how to invoke each callback.</param>
    protected virtual void InvokeCallbacks<TDelegate>(string key, Action<TDelegate> invoker) where TDelegate : Delegate
    {
        if (this.Signals.TryGetValue(key, out List<Delegate>? list))
        {
            foreach (Delegate callback in list)
            {
                if (callback is TDelegate typedCallback)
                {
                    invoker(typedCallback);
                }
            }
        }
    }
}
