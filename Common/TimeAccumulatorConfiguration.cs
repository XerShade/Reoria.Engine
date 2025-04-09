using Microsoft.Extensions.Configuration;

namespace Reoria.Engine.Common;

public readonly struct TimeAccumulatorConfiguration
{
    public readonly double UpdatesPerSecond;
    public readonly int MaxUpdatesPerFrame;

    public TimeAccumulatorConfiguration()
    {
        this.UpdatesPerSecond = 1 / 60;
        this.MaxUpdatesPerFrame = 5;
    }

    public TimeAccumulatorConfiguration(IConfigurationSection configuration) : this()
    {
        this.UpdatesPerSecond = 1 / Convert.ToDouble(configuration["UpdatesPerSecond"] ?? "60");
        this.MaxUpdatesPerFrame = Convert.ToInt32(configuration["MaxUpdatesPerFrame"] ?? "5");
    }

    public TimeAccumulatorConfiguration(double updatesPerSecond, int maxUpdatesPerFrame)
    {
        this.UpdatesPerSecond = updatesPerSecond;
        this.MaxUpdatesPerFrame = maxUpdatesPerFrame;
    }
}
