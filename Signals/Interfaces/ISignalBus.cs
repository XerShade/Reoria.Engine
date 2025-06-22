namespace Reoria.Engine.Signals.Interfaces;

/// <summary>
/// Defines the contract for a signal bus system that allows connecting, disconnecting,
/// and emitting signals identified by string keys with simple callback actions.
/// </summary>
public partial interface ISignalBus
{
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect(string key, Action callback);

    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect(string key, Action callback);

    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <param name="key">The unique string identifier for the signal.</param>
    void Emit(string key);
}
