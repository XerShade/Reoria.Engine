using Microsoft.Extensions.Configuration;
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

            this.SetupSerilog(configurationSources.GetConfiguration());
        }
    }

    protected SerilogLoggerFactory()
    {
        // Do nothing in this constructor, it merely exists for classes that inherit this class.
    }

    protected virtual void SetupSerilog(IConfiguration configuration)
    {
        Log.CloseAndFlush();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        this.LoggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
    }

    protected override void FreeUnmanagedObjects()
    {
        Log.CloseAndFlush();

        base.FreeUnmanagedObjects();
    }
}
