using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container.Configuration;
using Reoria.Engine.Container.Configuration.Interfaces;

namespace Reoria.Engine.Container.Logging;

public sealed class StartupLoggerFactory : SerilogLoggerFactory
{
    public StartupLoggerFactory(IServiceProvider serviceProvider) : base()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            IEngineConfigurationProvider configurationProvider = serviceProvider.GetRequiredService<IEngineConfigurationProvider>();

            this.SetupSerilog(new StartupConfigurationSources(configurationProvider).GetConfiguration());
        }
    }
}
