using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace Reoria.Engine.Base.Container.Logging;

public class SerilogLoggingInitializer : ContainerLoggingInitializer
{
    public SerilogLoggingInitializer() : base()
    {
    }

    public SerilogLoggingInitializer(IConfiguration configuration) : base(configuration)
    {
    }

    public override ILoggerFactory Initialize()
    {
        lock(this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            Log.CloseAndFlush();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.configuration)
                .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
            return loggerFactory;
        }
    }

    protected override void FreeUnmanagedObjects()
    {
        lock(this.@lock)
        {
            Log.CloseAndFlush();

            base.FreeUnmanagedObjects();
        }
    }
}
