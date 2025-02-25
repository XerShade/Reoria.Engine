using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Reoria.Engine.StateMachines.Interfaces;

/// <summary>
/// Represents a state within a state machine. A state encapsulates specific behavior, logic, 
/// and transitions associated with a particular mode or phase of a system (e.g., game or application).
/// This interface defines the essential lifecycle and update methods that states must implement.
/// </summary>
public interface IState
{
    /// <summary>
    /// Gets or sets the state machine that this state belongs to. 
    /// This provides the context for transitioning between states.
    /// </summary>
    IStateMachine? StateMachine { get; set; }

    /// <summary>
    /// Called when the state is entered. This method is used to initialize or prepare state-specific logic 
    /// and resources when the state becomes active.
    /// </summary>
    void Enter();

    /// <summary>
    /// Called when the state is exited. This method is used to clean up resources, finalize logic, 
    /// or prepare for transitioning out of the state.
    /// </summary>
    void Exit();

    /// <summary>
    /// Fixed update method called at fixed intervals. Typically used for physics calculations, 
    /// time-sensitive updates, or logic that should run at a consistent rate regardless of frame rate.
    /// </summary>
    /// <param name="gameTime">Provides timing information such as the elapsed time since the last fixed update.</param>
    void FixedUpdate(GameTime gameTime);

    /// <summary>
    /// Update method called every frame. This method is used to handle state-specific logic 
    /// such as gameplay updates, AI behaviors, input handling, and other real-time updates.
    /// </summary>
    /// <param name="gameTime">Provides timing information such as the elapsed time since the last update.</param>
    void Update(GameTime gameTime);

    /// <summary>
    /// Draw method called every frame to render the state’s visuals. This method is responsible for 
    /// drawing 2D sprites, UI elements, animations, or any other graphical content specific to the state.
    /// </summary>
    /// <param name="gameTime">Provides timing information such as the elapsed time since the last update.</param>
    /// <param name="spriteBatch">The SpriteBatch instance used to draw 2D sprites to the screen.</param>
    /// <param name="contentManager">The ContentManager used for loading and managing game assets like textures, fonts, etc.</param>
    void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager contentManager);
}
