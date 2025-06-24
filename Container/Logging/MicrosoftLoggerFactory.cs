using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Logging.Interfaces;
using MsLoggerFactory = Microsoft.Extensions.Logging.LoggerFactory;

namespace Reoria.Engine.Container.Logging;

public class MicrosoftLoggingInitializer : BaseLoggerFactory
{
    public override IEngineLoggerFactory SetupFactory(IConfiguration configuration)
    {
        this.SetupMicrosoftLogging(configuration);

        return this;
    }

    protected virtual void SetupMicrosoftLogging(IConfiguration configuration) 
        => this.LoggerFactory = MsLoggerFactory.Create(builder => builder.AddConfiguration(configuration.GetSection("Logging")).AddConsole());
}
