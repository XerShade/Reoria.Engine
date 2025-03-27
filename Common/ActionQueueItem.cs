namespace Reoria.Engine.Common;

/// <summary>
/// Represents an item in the action queue, containing an executable delegate,
/// its priority, arguments, and retry tracking.
/// </summary>
/// <param name="action">The delegate action to execute.</param>
/// <param name="priority">The priority level of the action.</param>
/// <param name="maxRetries">The maximum number of retries allowed if the action fails.</param>
/// <param name="args">Optional arguments to be passed when invoking the action.</param>
/// <exception cref="ArgumentNullException">Thrown if the provided action is null.</exception>
public class ActionQueueItem(Delegate action, int priority, int maxRetries, params object[] args)
{
    /// <summary>
    /// Gets the delegate action to be executed.
    /// </summary>
    public Delegate Action { get; } = action ?? throw new ArgumentNullException(nameof(action));

    /// <summary>
    /// Gets the arguments that will be passed when invoking the action.
    /// </summary>
    public object[] Args { get; } = args;

    /// <summary>
    /// Gets the priority of this action in the queue.
    /// Lower values indicate higher priority.
    /// </summary>
    public int Priority { get; } = priority;

    /// <summary>
    /// Gets or sets the remaining number of retries if the action fails.
    /// </summary>
    public int RetriesLeft { get; set; } = maxRetries;
}
