using Microsoft.Extensions.Logging;
using Reoria.Engine.MonoGame.Interfaces;
using Reoria.Engine.StateMachines.GameStates.Interfaces;
using Reoria.Engine.StateMachines.GameStates.States;
using Reoria.Engine.StateMachines.Interfaces;

namespace Reoria.Engine.StateMachines.GameStates;

/// <summary>
/// Represents a game state machine that manages game states and their transitions.
/// </summary>
/// <param name="logger">Logger to log information for debugging purposes.</param>
/// <param name="serviceProvider">The service provider used for dependency injection, allowing access to services throughout the application.</param>
public class GameStateMachine(ILogger<IStateMachine> logger, IServiceProvider serviceProvider) : StateMachine<IGameState>(logger, serviceProvider), IGameStateMachine
{
    public virtual IXnaGame? Game { get; protected set; }

    public virtual void AttachToWindow(IXnaGame xnaGame) => this.Game = xnaGame;
}
