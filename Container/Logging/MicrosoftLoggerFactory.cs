using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Configuration.Interfaces;
using MsLoggerFactory = Microsoft.Extensions.Logging.LoggerFactory;

namespace Reoria.Engine.Container.Logging;

public class MicrosoftLoggingInitializer : BaseLoggerFactory
{
    public MicrosoftLoggingInitializer(IEngineConfigurationSources configurationSources)
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            this.LoggerFactory = MsLoggerFactory.Create(builder => builder.AddConfiguration(configurationSources.GetConfiguration().GetSection("Logging")).AddConsole());
        }
    }
}
