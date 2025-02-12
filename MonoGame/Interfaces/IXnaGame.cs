using Microsoft.Xna.Framework;
using Reoria.Engine.StateMachines.Interfaces;

namespace Reoria.Engine.MonoGame.Interfaces;

public interface IXnaGame : IDisposable
{
    GameServiceContainer Services { get; }
    void Run();
    void Exit();
    void SetPerformanceSettings(int fixedUpdateRate, int maxFixedSteps, int newMaxFPS, bool enableVSync);
    IXnaGame ChangeState<TState>() where TState : class, IState, new();
}
