using Microsoft.Xna.Framework;

namespace Reoria.Engine.MonoGame.Interfaces;

public interface IXnaGame : IDisposable
{
    GameServiceContainer Services { get; }
    void Run();
    void SetPerformanceSettings(int newMaxFPS, int fixedUpdateRate, int maxFixedSteps, bool enableVSync);
}
