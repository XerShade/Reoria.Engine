using Reoria.Engine.MonoGame.Interfaces;
using Reoria.Engine.StateMachines.GameStates.States;
using Reoria.Engine.StateMachines.Interfaces;

namespace Reoria.Engine.StateMachines.GameStates.Interfaces;

public interface IGameStateMachine : IStateMachine<IGameState>
{
    IXnaGame? Game { get; }

    void AttachToWindow(IXnaGame xnaGame);
}
