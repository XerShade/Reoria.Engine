using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container.Configuration.Interfaces;

namespace Reoria.Engine.Container.Logging;

public sealed class StartupLoggerFactory : SerilogLoggerFactory
{
    public StartupLoggerFactory(IServiceProvider serviceProvider) : base()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            IEngineConfigurationSources configurationSources = serviceProvider.GetRequiredService<IEngineConfigurationSources>();

            configurationSources.AddSource("appsettings.json", optional: false, reloadOnChange: true);

            this.SetupSerilog(configurationSources.GetConfiguration());
        }
    }
}