using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Configuration.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;

namespace Reoria.Engine.Container.Logging;

public class SerilogLoggerFactory : BaseLoggerFactory
{
    public SerilogLoggerFactory(IEngineConfigurationSources configurationSources)
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            Log.CloseAndFlush();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationSources.GetConfiguration())
                .CreateLogger();

            this.LoggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
        }
    }

    protected override void FreeUnmanagedObjects()
    {
        Log.CloseAndFlush();

        base.FreeUnmanagedObjects();
    }
}
