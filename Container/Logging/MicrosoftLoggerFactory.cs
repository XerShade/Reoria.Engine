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

            this.SetupMicrosoftLogging(configurationSources.GetConfiguration());
        }
    }

    protected MicrosoftLoggingInitializer()
    {
        // Do nothing in this constructor, it merely exists for classes that inherit this class.
    }

    protected virtual void SetupMicrosoftLogging(IConfiguration configuration) 
        => this.LoggerFactory = MsLoggerFactory.Create(builder => builder.AddConfiguration(configuration.GetSection("Logging")).AddConsole());
}
