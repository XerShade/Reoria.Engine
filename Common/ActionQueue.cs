namespace Reoria.Engine.Common;

/// <summary>
/// Represents a queue system for executing actions with priority and retry capabilities.
/// </summary>
public class ActionQueue : Disposable, IDisposable
{
    /// <summary>
    /// The priority queue that holds the actions to be executed, sorted by priority.
    /// </summary>
    protected readonly PriorityQueue<ActionQueueItem, int> Queue = new();

    /// <summary>
    /// A secondary queue used to store actions that failed and need to be retried.
    /// </summary>
    protected readonly Queue<ActionQueueItem> RetryQueue = new();

    /// <summary>
    /// Releases managed resources, clearing both the main queue and retry queue.
    /// </summary>
    protected override void FreeManagedObjects()
    {
        // Invoke the base class method.
        base.FreeManagedObjects();

        // Clear the queue and retry queue.
        this.Queue.Clear();
        this.RetryQueue.Clear();
    }

    /// <summary>
    /// Enqueues an action with default priority and retry settings.
    /// </summary>
    /// <param name="action">The delegate action to enqueue.</param>
    public virtual void Enqueue(Delegate action)
        => this.Enqueue(action, 5, 5);

    /// <summary>
    /// Enqueues an action with a specified priority and default retry settings.
    /// </summary>
    /// <param name="action">The delegate action to enqueue.</param>
    /// <param name="priority">The priority level of the action.</param>
    public virtual void Enqueue(Delegate action, int priority)
        => this.Enqueue(action, priority, 5);

    /// <summary>
    /// Enqueues an action with a specified priority and maximum retries.
    /// </summary>
    /// <param name="action">The delegate action to enqueue.</param>
    /// <param name="priority">The priority level of the action.</param>
    /// <param name="maxRetries">The maximum number of retry attempts.</param>
    public virtual void Enqueue(Delegate action, int priority, int maxRetries)
        => this.Enqueue(action, priority, maxRetries, Array.Empty<object>());

    /// <summary>
    /// Enqueues an action with default priority and retry settings, along with arguments.
    /// </summary>
    /// <param name="action">The delegate action to enqueue.</param>
    /// <param name="args">The arguments to pass when invoking the action.</param>
    public virtual void Enqueue(Delegate action, params object[] args)
        => this.Enqueue(action, 5, 5, args);

    /// <summary>
    /// Enqueues an action with a specified priority and arguments.
    /// </summary>
    /// <param name="action">The delegate action to enqueue.</param>
    /// <param name="priority">The priority level of the action.</param>
    /// <param name="args">The arguments to pass when invoking the action.</param>
    public virtual void Enqueue(Delegate action, int priority, params object[] args)
        => this.Enqueue(action, priority, 5, args);

    /// <summary>
    /// Enqueues an action with a specified priority, maximum retries, and arguments.
    /// </summary>
    /// <param name="action">The delegate action to enqueue.</param>
    /// <param name="priority">The priority level of the action.</param>
    /// <param name="maxRetries">The maximum number of retry attempts.</param>
    /// <param name="args">The arguments to pass when invoking the action.</param>
    /// <exception cref="ArgumentNullException">Thrown if the action is null.</exception>
    public virtual void Enqueue(Delegate action, int priority, int maxRetries, params object[] args)
    {
        // Make sure that a valid delegate is being passed in.
        ArgumentNullException.ThrowIfNull(action);

        // Ensure thread-safe execution by acquiring the lock.
        lock (this.@lock)
        {
            // Add the action to the queue to be processed later.
            this.Queue.Enqueue(new ActionQueueItem(action, priority, maxRetries, args), priority);
        }
    }

    /// <summary>
    /// Processes a single queue item, executing the action.
    /// If execution fails, the action may be enqueued into the retry queue based on remaining retries.
    /// </summary>
    public virtual void ProcessQueueItem()
    {
        // Ensure thread-safe execution by acquiring the lock.
        lock (this.@lock)
        {
            // Attempt to get the action out of the queue.
            if (this.Queue.TryDequeue(out ActionQueueItem? item, out _))
            {
                try
                {
                    // Attempt to invoke the action.
                    _ = item.Action.DynamicInvoke(item.Args);
                }
                catch (Exception)
                {
                    // Check to see if the action has any retry attempts left.
                    if (item.RetriesLeft > 0)
                    {
                        // Lower the retry count of the action, and add it to the retry queue.
                        item.RetriesLeft--;
                        this.RetryQueue.Enqueue(item);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Moves failed actions from the retry queue back to the main queue for re-execution.
    /// </summary>
    public virtual void ProcessRetryQueue()
    {
        // Ensure thread-safe execution by acquiring the lock.
        lock (this.@lock)
        {
            // Check to see if there are any actions in retry queue to process.
            while (this.RetryQueue.Count > 0)
            {
                // Attempt to get the action out of the retry queue.
                if (this.RetryQueue.TryDequeue(out ActionQueueItem? item))
                {
                    // Re-enqueue the action on the retry queue.
                    this.Queue.Enqueue(item, item.Priority);
                }
            }
        }
    }

    /// <summary>
    /// Processes all queued actions until the queue is empty.
    /// Afterward, it attempts to process failed actions from the retry queue.
    /// </summary>
    public virtual void ProcessQueue()
    {
        // Check to see if there are any actions in queue to process.
        while (this.Queue.Count > 0)
        {
            // Process the action.
            this.ProcessQueueItem();
        }

        // Re-queue any actions that failed to execute.
        this.ProcessRetryQueue();
    }
}
