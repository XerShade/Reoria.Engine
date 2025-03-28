using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Logging.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;

namespace Reoria.Engine.Container.Logging;

public class SerilogLoggingInitializer : Disposable, ILoggingInitializer
{
    protected IConfiguration configuration;
    protected bool isConfigurationBuilt;

    public SerilogLoggingInitializer()
    {
        this.configuration = new ConfigurationBuilder().Build();
        this.isConfigurationBuilt = false;
    }

    public SerilogLoggingInitializer(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.isConfigurationBuilt = true;
    }

    protected override void FreeUnmanagedObjects()
    {
        Log.CloseAndFlush();

        base.FreeUnmanagedObjects();
    }

    protected virtual IConfiguration GetLoggerConfiguration()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        if (!this.isConfigurationBuilt)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            this.isConfigurationBuilt = true;

            return builder.Build();
        }

        return this.configuration;
    }

    public virtual ILoggerFactory Initialize()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            Log.CloseAndFlush();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.GetLoggerConfiguration())
                .CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
            return loggerFactory;
        }
    }
}
