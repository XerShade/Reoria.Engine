using Microsoft.Xna.Framework;

namespace Reoria.Engine.StateMachines.Interfaces;

/// <summary>
/// Defines a basic state machine that manages state transitions and updates.
/// </summary>
public interface IStateMachine
{
    /// <summary>
    /// Gets the current state of the state machine.
    /// </summary>
    IState? CurrentState { get; }
    /// <summary>
    /// Gets the previous state of the state machine.
    /// </summary>
    IState? PreviousState { get; }

    /// <summary>
    /// Changes the current state of the state machine to a new state.
    /// </summary>
    /// <typeparam name="TState">The type of the state to transition to. It must be a class that implements <see cref="IState"/> and has a parameterless constructor.</typeparam>
    void ChangeState<TState>() where TState : class, IState, new();
    /// <summary>
    /// Updates the state machine in the fixed update cycle. This is where time-based logic (such as physics or animations) is often processed.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void FixedUpdate(GameTime gameTime);
    /// <summary>
    /// Updates the state machine in the regular update cycle. This is where logic such as input handling and non-time-dependent updates typically occur.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void Update(GameTime gameTime);
    /// <summary>
    /// Draws the current state to the screen. This is where rendering code is typically handled.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void Draw(GameTime gameTime);
}
