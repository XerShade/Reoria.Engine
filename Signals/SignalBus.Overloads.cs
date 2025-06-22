namespace Reoria.Engine.Signals;

/// <summary>
/// Implements a simple signal bus system for managing named events and their subscribers.
/// Allows clients to connect callbacks to signals, emit signals to invoke callbacks,
/// and disconnect callbacks when no longer needed.
/// </summary>
public partial class SignalBus
{
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1>(string key, Action<T1> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1>(string key, Action<T1> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    public virtual void Emit<T1>(string key, T1 arg1) => this.InvokeCallbacks<Action<T1>>(key, cb => cb(arg1));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2>(string key, Action<T1, T2> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2>(string key, Action<T1, T2> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    public virtual void Emit<T1, T2>(string key, T1 arg1, T2 arg2) => this.InvokeCallbacks<Action<T1, T2>>(key, cb => cb(arg1, arg2));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3>(string key, Action<T1, T2, T3> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3>(string key, Action<T1, T2, T3> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3) => this.InvokeCallbacks<Action<T1, T2, T3>>(key, cb => cb(arg1, arg2, arg3));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => this.InvokeCallbacks<Action<T1, T2, T3, T4>>(key, cb => cb(arg1, arg2, arg3, arg4));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5>(string key, Action<T1, T2, T3, T4, T5> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5>(string key, Action<T1, T2, T3, T4, T5> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6>(string key, Action<T1, T2, T3, T4, T5, T6> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6>(string key, Action<T1, T2, T3, T4, T5, T6> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7>(string key, Action<T1, T2, T3, T4, T5, T6, T7> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7>(string key, Action<T1, T2, T3, T4, T5, T6, T7> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback) => this.RemoveCallback(key, callback);

    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    /// <param name="arg11">The 11th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    /// <param name="arg11">The 11th argument to pass to the callback.</param>
    /// <param name="arg12">The 12th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    /// <param name="arg11">The 11th argument to pass to the callback.</param>
    /// <param name="arg12">The 12th argument to pass to the callback.</param>
    /// <param name="arg13">The 13th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    /// <param name="arg11">The 11th argument to pass to the callback.</param>
    /// <param name="arg12">The 12th argument to pass to the callback.</param>
    /// <param name="arg13">The 13th argument to pass to the callback.</param>
    /// <param name="arg14">The 14th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the 15th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the 15th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the 15th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    /// <param name="arg11">The 11th argument to pass to the callback.</param>
    /// <param name="arg12">The 12th argument to pass to the callback.</param>
    /// <param name="arg13">The 13th argument to pass to the callback.</param>
    /// <param name="arg14">The 14th argument to pass to the callback.</param>
    /// <param name="arg15">The 15th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));

    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the 15th argument for the callback.</typeparam>
    /// <typeparam name="T16">Type of the 16th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    public virtual void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback) => this.AddCallback(key, callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the 15th argument for the callback.</typeparam>
    /// <typeparam name="T16">Type of the 16th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    public virtual void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback) => this.RemoveCallback(key, callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the 1st argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the 2nd argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the 3rd argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the 4th argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the 5th argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the 6th argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the 7th argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the 8th argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the 9th argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the 10th argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the 11th argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the 12th argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the 13th argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the 14th argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the 15th argument for the callback.</typeparam>
    /// <typeparam name="T16">Type of the 16th argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The 1st argument to pass to the callback.</param>
    /// <param name="arg2">The 2nd argument to pass to the callback.</param>
    /// <param name="arg3">The 3rd argument to pass to the callback.</param>
    /// <param name="arg4">The 4th argument to pass to the callback.</param>
    /// <param name="arg5">The 5th argument to pass to the callback.</param>
    /// <param name="arg6">The 6th argument to pass to the callback.</param>
    /// <param name="arg7">The 7th argument to pass to the callback.</param>
    /// <param name="arg8">The 8th argument to pass to the callback.</param>
    /// <param name="arg9">The 9th argument to pass to the callback.</param>
    /// <param name="arg10">The 10th argument to pass to the callback.</param>
    /// <param name="arg11">The 11th argument to pass to the callback.</param>
    /// <param name="arg12">The 12th argument to pass to the callback.</param>
    /// <param name="arg13">The 13th argument to pass to the callback.</param>
    /// <param name="arg14">The 14th argument to pass to the callback.</param>
    /// <param name="arg15">The 15th argument to pass to the callback.</param>
    /// <param name="arg16">The 16th argument to pass to the callback.</param>
    public virtual void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16) => this.InvokeCallbacks<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(key, cb => cb(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));

}