using Reoria.Engine.StateMachines.GameStates.Interfaces;
using Reoria.Engine.StateMachines.Interfaces;

namespace Reoria.Engine.StateMachines.GameStates.States;

/// <summary>
/// Represents a game state within a state machine. A game state encapsulates specific 
/// behavior and transitions related to a particular stage or mode of the game.
/// It extends from <see cref="IState"/> to ensure compatibility with the state machine system.
/// </summary>
public interface IGameState : IState
{
    IGameStateMachine? GameStateMachine { get; set; }
}
