using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Logging.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;

namespace Reoria.Engine.Container.Logging;

public class SerilogLoggingInitializer(IConfiguration configuration) : Disposable, ILoggingInitializer
{
    protected IConfiguration Configuration = configuration;

    protected override void FreeUnmanagedObjects()
    {
        Log.CloseAndFlush();

        base.FreeUnmanagedObjects();
    }

    public virtual ILoggerFactory CreateLoggerFactory()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            Log.CloseAndFlush();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
            return loggerFactory;
        }
    }
}
