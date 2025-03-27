using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Logging.Interfaces;

namespace Reoria.Engine.Container.Logging;

public class MicrosoftLoggingInitializer : Disposable, ILoggingInitializer
{
    protected IConfiguration configuration;
    protected bool isConfigurationBuilt;

    public MicrosoftLoggingInitializer()
    {
        this.configuration = new ConfigurationBuilder().Build();
        this.isConfigurationBuilt = false;
    }

    public MicrosoftLoggingInitializer(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.isConfigurationBuilt = true;
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

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConfiguration(this.GetLoggerConfiguration().GetSection("Logging")).AddConsole());
            return loggerFactory;
        }
    }
}
