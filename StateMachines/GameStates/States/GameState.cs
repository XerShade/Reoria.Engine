using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reoria.Engine.StateMachines.GameStates.Interfaces;
using Reoria.Engine.StateMachines.Interfaces;

namespace Reoria.Engine.StateMachines.GameStates.States;

/// <summary>
/// Represents a game state within a state machine. 
/// The state is responsible for handling its specific logic during gameplay, 
/// such as initialization, updates, and rendering. It can be entered and exited 
/// within the state machine's lifecycle.
/// </summary>
public abstract class GameState : IGameState
{
    /// <summary>
    /// Gets or sets the state machine that this state belongs to. 
    /// This provides access to transition between states.
    /// </summary>
    public virtual IStateMachine? StateMachine { get; set; }

    /// <summary>
    /// Gets or sets the game state machine that this state belongs to. 
    /// This provides access to transition between states and other game specific functions.
    /// </summary>
    public virtual IGameStateMachine? GameStateMachine { get => this.StateMachine as IGameStateMachine; set => this.StateMachine = value as IStateMachine; }

    /// <summary>
    /// Method called when the state is entered. This method is usually used to 
    /// initialize state-specific resources, logic, or variables. 
    /// It is invoked once when the state is activated.
    /// </summary>
    public virtual void Enter() { }

    /// <summary>
    /// Method called when the state is exited. This method is typically used to 
    /// clean up or release resources, finalize logic, or prepare for transitioning to another state.
    /// It is invoked just before the state is deactivated.
    /// </summary>
    public virtual void Exit() { }

    /// <summary>
    /// Fixed update method called at regular, fixed intervals. This method is ideal for 
    /// handling physics, time-sensitive logic, or other calculations that need to be updated at 
    /// consistent intervals (e.g., frame-rate-independent).
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last fixed update.</param>
    public virtual void FixedUpdate(GameTime gameTime) { }

    /// <summary>
    /// Update method called every frame. This method is used for updating state-specific logic 
    /// such as gameplay updates, AI logic, input processing, or other real-time behavior.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    public virtual void Update(GameTime gameTime) { }

    /// <summary>
    /// Draw method called every frame to render the visuals of the state. 
    /// It is responsible for drawing state-specific graphics, animations, or other UI elements.
    /// </summary>
    /// <param name="gameTime">Provides game timing information, such as elapsed time since the last update.</param>
    /// <param name="spriteBatch">The SpriteBatch used to render 2D sprites to the screen.</param>
    /// <param name="contentManager">The ContentManager used for loading assets such as textures, fonts, etc.</param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager contentManager) { }
}
