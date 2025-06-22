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
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1>(string key, Action<T1> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1>(string key, Action<T1> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    void Emit<T1>(string key, T1 arg1);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2>(string key, Action<T1, T2> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2>(string key, Action<T1, T2> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    void Emit<T1, T2>(string key, T1 arg1, T2 arg2);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3>(string key, Action<T1, T2, T3> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3>(string key, Action<T1, T2, T3> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    void Emit<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5>(string key, Action<T1, T2, T3, T4, T5> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5>(string key, Action<T1, T2, T3, T4, T5> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6>(string key, Action<T1, T2, T3, T4, T5, T6> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6>(string key, Action<T1, T2, T3, T4, T5, T6> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7>(string key, Action<T1, T2, T3, T4, T5, T6, T7> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7>(string key, Action<T1, T2, T3, T4, T5, T6, T7> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    /// <param name="arg11">The eleventh argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    /// <param name="arg11">The eleventh argument to pass to the callback.</param>
    /// <param name="arg12">The twelfth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    /// <param name="arg11">The eleventh argument to pass to the callback.</param>
    /// <param name="arg12">The twelfth argument to pass to the callback.</param>
    /// <param name="arg13">The thirteenth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    /// <param name="arg11">The eleventh argument to pass to the callback.</param>
    /// <param name="arg12">The twelfth argument to pass to the callback.</param>
    /// <param name="arg13">The thirteenth argument to pass to the callback.</param>
    /// <param name="arg14">The fourteenth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the fifteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key,Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the fifteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key,Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the fifteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    /// <param name="arg11">The eleventh argument to pass to the callback.</param>
    /// <param name="arg12">The twelfth argument to pass to the callback.</param>
    /// <param name="arg13">The thirteenth argument to pass to the callback.</param>
    /// <param name="arg14">The fourteenth argument to pass to the callback.</param>
    /// <param name="arg15">The fifteenth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key,T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15);
    
    /// <summary>
    /// Connects (registers) a callback action to a signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the fifteenth argument for the callback.</typeparam>
    /// <typeparam name="T16">Type of the sixteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to invoke when the signal is emitted.</param>
    void Connect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key,Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback);
    /// <summary>
    /// Disconnects (removes) a previously registered callback from the signal identified by the specified key.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the fifteenth argument for the callback.</typeparam>
    /// <typeparam name="T16">Type of the sixteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="callback">The callback to remove.</param>
    void Disconnect<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key,Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback);
    /// <summary>
    /// Emits (triggers) the signal identified by the specified key,
    /// causing all connected callbacks to be invoked.
    /// </summary>
    /// <typeparam name="T1">Type of the first argument for the callback.</typeparam>
    /// <typeparam name="T2">Type of the second argument for the callback.</typeparam>
    /// <typeparam name="T3">Type of the third argument for the callback.</typeparam>
    /// <typeparam name="T4">Type of the fourth argument for the callback.</typeparam>
    /// <typeparam name="T5">Type of the fifth argument for the callback.</typeparam>
    /// <typeparam name="T6">Type of the sixth argument for the callback.</typeparam>
    /// <typeparam name="T7">Type of the seventh argument for the callback.</typeparam>
    /// <typeparam name="T8">Type of the eighth argument for the callback.</typeparam>
    /// <typeparam name="T9">Type of the ninth argument for the callback.</typeparam>
    /// <typeparam name="T10">Type of the tenth argument for the callback.</typeparam>
    /// <typeparam name="T11">Type of the eleventh argument for the callback.</typeparam>
    /// <typeparam name="T12">Type of the twelfth argument for the callback.</typeparam>
    /// <typeparam name="T13">Type of the thirteenth argument for the callback.</typeparam>
    /// <typeparam name="T14">Type of the fourteenth argument for the callback.</typeparam>
    /// <typeparam name="T15">Type of the fifteenth argument for the callback.</typeparam>
    /// <typeparam name="T16">Type of the sixteenth argument for the callback.</typeparam>
    /// <param name="key">The unique string identifier for the signal.</param>
    /// <param name="arg1">The first argument to pass to the callback.</param>
    /// <param name="arg2">The second argument to pass to the callback.</param>
    /// <param name="arg3">The third argument to pass to the callback.</param>
    /// <param name="arg4">The fourth argument to pass to the callback.</param>
    /// <param name="arg5">The fifth argument to pass to the callback.</param>
    /// <param name="arg6">The sixth argument to pass to the callback.</param>
    /// <param name="arg7">The seventh argument to pass to the callback.</param>
    /// <param name="arg8">The eighth argument to pass to the callback.</param>
    /// <param name="arg9">The ninth argument to pass to the callback.</param>
    /// <param name="arg10">The tenth argument to pass to the callback.</param>
    /// <param name="arg11">The eleventh argument to pass to the callback.</param>
    /// <param name="arg12">The twelfth argument to pass to the callback.</param>
    /// <param name="arg13">The thirteenth argument to pass to the callback.</param>
    /// <param name="arg14">The fourteenth argument to pass to the callback.</param>
    /// <param name="arg15">The fifteenth argument to pass to the callback.</param>
    /// <param name="arg16">The sixteenth argument to pass to the callback.</param>
    void Emit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key,T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16);
}