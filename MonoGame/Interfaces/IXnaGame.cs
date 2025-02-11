using Microsoft.Xna.Framework;

namespace Reoria.Engine.MonoGame.Interfaces;

public interface IXnaGame : IDisposable
{
    GameServiceContainer Services { get; }
    void Run();
    void Exit();
    void SetPerformanceSettings(int fixedUpdateRate, int maxFixedSteps, int newMaxFPS, bool enableVSync);
}
