using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Logging.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;

namespace Reoria.Engine.Container.Logging;

public class SerilogLoggerFactory : BaseLoggerFactory
{
    protected override void FreeUnmanagedObjects()
    {
        Log.CloseAndFlush();

        base.FreeUnmanagedObjects();
    }

    public override IEngineLoggerFactory SetupFactory(IConfiguration configuration)
    {
        this.SetupSerilog(configuration);

        return this;
    }

    protected virtual void SetupSerilog(IConfiguration configuration)
    {
        Log.CloseAndFlush();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        this.LoggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
    }
}
