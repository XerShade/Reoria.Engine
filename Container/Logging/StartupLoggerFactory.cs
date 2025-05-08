using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Configuration;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Logging.Interfaces;
using Serilog;
using Serilog.Extensions.Logging;

namespace Reoria.Engine.Container.Logging;

public class StartupLoggerFactory : Disposable, IEngineLoggerFactory
{
    protected readonly ILoggerFactory LoggerFactory;

    public StartupLoggerFactory(IServiceProvider serviceProvider)
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            IEngineConfigurationProvider configurationProvider = serviceProvider.GetRequiredService<IEngineConfigurationProvider>();
            IEngineConfigurationSources configurationSources = new StartupConfigurationSources(configurationProvider);

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

    public virtual ILoggerFactory GetLoggerFactory()
        => this.LoggerFactory;

    public virtual ILogger<T> GetLogger<T>()
        => this.LoggerFactory.CreateLogger<T>();
}
