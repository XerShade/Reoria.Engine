using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Reoria.Engine.StateMachines.Interfaces;

namespace Reoria.Engine.StateMachines;

/// <summary>
/// Represents a state machine that manages states and their transitions.
/// </summary>
public class StateMachine(ILogger<IStateMachine> logger, IServiceProvider serviceProvider) : IStateMachine
{
    /// <summary>
    /// Logger to log information for debugging purposes.
    /// </summary>
    protected readonly ILogger<IStateMachine> logger = logger;
    /// <summary>
    /// Cache of states to avoid creating multiple instances of the same state.
    /// </summary>
    protected readonly List<IState> StateCache = [];
    /// <summary>
    /// Lock object to ensure thread safety during state transitions and updates.
    /// </summary>
    protected readonly Lock @lock = new();

    /// <summary>
    /// The current active state of the state machine.
    /// </summary>
    public IState? CurrentState { get; protected set; }
    /// <summary>
    /// The previously active state of the state machine.
    /// </summary>
    public IState? PreviousState { get; protected set; }
    /// <summary>
    /// The service provider used for dependency injection, allowing access to services throughout the application.
    /// </summary>
    public IServiceProvider ServiceProvider { get; protected set; } = serviceProvider;

    /// <summary>
    /// Changes the current state of the state machine to a new state.
    /// </summary>
    /// <typeparam name="TState">The type of the state to transition to. It must be a class that implements <see cref="IState"/> and has a parameterless constructor.</typeparam>
    public virtual void ChangeState<TState>() where TState : class, IState, new()
    {
        lock (this.@lock)
        {
            // If there is an existing state, exit it and log the transition.
            if (this.CurrentState != null)
            {
                this.CurrentState?.Exit();
                this.PreviousState = this.CurrentState;
                this.logger.LogDebug("Changing state on state machine {machine} from {previous} to {new}.",
                    this.GetType().Name, this.PreviousState?.GetType().Name, typeof(TState).Name);
            }
            else
            {
                // Log the first state change if there was no previous state.
                this.logger.LogDebug("Changing state on state machine {machine} to {new}.",
                    this.GetType().Name, typeof(TState).Name);
            }

            // Attempt to find the state in the cache.
            TState? cachedState = (from s in this.StateCache
                                   where s.GetType() == typeof(TState)
                                   select s as TState).FirstOrDefault();

            // If the state is not in the cache, create a new instance of it.
            if (cachedState == null)
            {
                try
                {
                    // Create a new instance of the state.
                    TState newState = Activator.CreateInstance<TState>();
                    newState.StateMachine = this;

                    // Add the new state to the cache and use it.
                    this.StateCache.Add(newState);
                    this.CurrentState = newState;
                }
                catch (Exception ex)
                {
                    // Log an error if state creation fails.
                    this.logger.LogError(ex, "Unable to create instance of state {TState}", typeof(TState).Name);
                }
            }
            else
            {
                // Use the cached state if it already exists.
                this.CurrentState = cachedState;
            }

            // Enter the new state.
            this.CurrentState?.Enter();
        }
    }

    /// <summary>
    /// Updates the state machine in the fixed update cycle. This is where time-based logic (such as physics or animations) is often processed.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    public virtual void FixedUpdate(GameTime gameTime)
    {
        lock (this.@lock)
        {
            // If there is a current state, invoke its FixedUpdate method.
            if (this.CurrentState != null)
            {
                this.CurrentState?.FixedUpdate(gameTime);
            }
        }
    }

    /// <summary>
    /// Updates the state machine in the regular update cycle. This is where logic such as input handling and non-time-dependent updates typically occur.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    public virtual void Update(GameTime gameTime)
    {
        lock (this.@lock)
        {
            // If there is a current state, invoke its Update method.
            if (this.CurrentState != null)
            {
                this.CurrentState?.Update(gameTime);
            }
        }
    }

    /// <summary>
    /// Draws the current state to the screen. This is where rendering code is typically handled.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    public virtual void Draw(GameTime gameTime)
    {
        lock (this.@lock)
        {
            // If there is a current state, invoke its Draw method.
            if (this.CurrentState != null)
            {
                this.CurrentState?.Draw(gameTime);
            }
        }
    }
}
