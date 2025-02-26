using Microsoft.Xna.Framework;
using Reoria.Engine.MonoGame.Camera.Interfaces;
using Reoria.Engine.StateMachines.GameStates.Interfaces;

namespace Reoria.Engine.MonoGame.Interfaces;

public interface IXnaGame : IDisposable
{
    GameServiceContainer Services { get; }
    IGameStateMachine StateMachine { get; }

    void Run();
    void Exit();
    void SetPerformanceSettings(int fixedUpdateRate, int maxFixedSteps, int newMaxFPS, bool enableVSync);
    Vector2 GetWindowSize();
    ICamera2D GetCamera();
}
