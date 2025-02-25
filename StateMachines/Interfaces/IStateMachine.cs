using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Reoria.Engine.StateMachines.Interfaces;

/// <summary>
/// Defines a basic state machine that manages state transitions, updates, and drawing.
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
    /// The service provider used for dependency injection, allowing access to services throughout the application.
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Changes the current state of the state machine to a new state.
    /// This triggers the state transition logic, including the entry and exit actions for states.
    /// </summary>
    /// <typeparam name="TState">The type of the state to transition to. It must be a class that implements <see cref="IState"/> and has a parameterless constructor.</typeparam>
    void ChangeState<TState>() where TState : class, IState, new();

    /// <summary>
    /// Updates the state machine in the fixed update cycle. 
    /// Typically used for time-based logic like physics or animations.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void FixedUpdate(GameTime gameTime);

    /// <summary>
    /// Updates the state machine in the regular update cycle. 
    /// Typically used for logic like input handling or non-time-dependent updates.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void Update(GameTime gameTime);

    /// <summary>
    /// Draws the current state to the screen.
    /// This is where rendering code, including drawing of sprites and UI, is typically handled.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    /// <param name="spriteBatch">The SpriteBatch used for rendering 2D sprites to the screen.</param>
    /// <param name="contentManager">The ContentManager used for loading assets like textures and fonts.</param>
    void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager contentManager);
}

/// <summary>
/// Defines a state machine that manages specific types of state transitions and updates.
/// This generic interface allows for stronger type safety when managing the states.
/// </summary>
/// <typeparam name="TStateType">The type of state this state machine handles. It must be a class that implements <see cref="IState"/> and has a parameterless constructor.</typeparam>
public interface IStateMachine<TStateType> where TStateType : IState
{
    /// <summary>
    /// Gets the current state of the state machine, strongly typed to <typeparamref name="TStateType"/>.
    /// </summary>
    TStateType? CurrentState { get; }

    /// <summary>
    /// Gets the previous state of the state machine, strongly typed to <typeparamref name="TStateType"/>.
    /// </summary>
    TStateType? PreviousState { get; }

    /// <summary>
    /// The service provider used for dependency injection, allowing access to services throughout the application.
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Changes the current state of the state machine to a new state.
    /// This triggers the state transition logic, including the entry and exit actions for states.
    /// </summary>
    /// <typeparam name="TState">The type of the state to transition to. It must be a class that implements <see cref="TStateType"/> and has a parameterless constructor.</typeparam>
    void ChangeState<TState>() where TState : class, TStateType, new();

    /// <summary>
    /// Updates the state machine in the fixed update cycle. 
    /// Typically used for time-based logic like physics or animations.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void FixedUpdate(GameTime gameTime);

    /// <summary>
    /// Updates the state machine in the regular update cycle. 
    /// Typically used for logic like input handling or non-time-dependent updates.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    void Update(GameTime gameTime);

    /// <summary>
    /// Draws the current state to the screen.
    /// This is where rendering code, including drawing of sprites and UI, is typically handled.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    /// <param name="spriteBatch">The SpriteBatch used for rendering 2D sprites to the screen.</param>
    /// <param name="contentManager">The ContentManager used for loading assets like textures and fonts.</param>
    void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager contentManager);
}