using Microsoft.Xna.Framework;

namespace Reoria.Engine.StateMachines.Interfaces;

/// <summary>
/// Represents a state within a state machine. A state encapsulates behavior and transitions.
/// </summary>
public interface IState
{
    /// <summary>
    /// The state machine that the state belongs to.
    /// </summary>
    IStateMachine? StateMachine { get; set; }

    /// <summary>
    /// Called when the state is entered. This method is typically used to initialize state-specific logic.
    /// </summary>
    void Enter();
    /// <summary>
    /// Called when the state is exited. This method is typically used to clean up or finalize logic before leaving the state.
    /// </summary>
    void Exit();

    /// <summary>
    /// Fixed update method called at fixed intervals, usually for physics or time-sensitive logic.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void FixedUpdate(GameTime gameTime);
    /// <summary>
    /// Update method called every frame to handle state-specific logic, such as gameplay or AI updates.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void Update(GameTime gameTime);
    /// <summary>
    /// Draw method called every frame to render the state, such as displaying visuals or other state-specific rendering.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void Draw(GameTime gameTime);
}
