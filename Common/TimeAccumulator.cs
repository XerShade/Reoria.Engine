using Microsoft.Xna.Framework;

namespace Reoria.Engine.Common;

public class TimeAccumulator(double timeStep, double maxUpdates) : Disposable, IDisposable, IAsyncDisposable
{
    public readonly double TimeStep = timeStep;
    public readonly double MaxUpdates = maxUpdates;

    public double Accumulator { get; protected set; } = 0d;
    public int UpdateCount { get; protected set; } = 0;

    public virtual void Update(GameTime gameTime, Action<GameTime> action)
    {
        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        this.Accumulator += deltaTime;
        this.UpdateCount = 0;

        while (this.Accumulator >= this.TimeStep && this.UpdateCount < this.MaxUpdates)
        {
            action.Invoke(new GameTime(gameTime.TotalGameTime, TimeSpan.FromSeconds(this.TimeStep), gameTime.IsRunningSlowly));
            this.Accumulator -= this.TimeStep;
            this.UpdateCount++;
        }
    }
}
