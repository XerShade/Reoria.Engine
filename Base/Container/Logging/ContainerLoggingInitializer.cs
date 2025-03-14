using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Base.Container.Logging.Interfaces;

namespace Reoria.Engine.Base.Container.Logging;

public class ContainerLoggingInitializer : Disposable, IContainerLoggingInitializer
{
    protected IConfiguration configuration;
    protected bool isConfigurationBuilt;

    public ContainerLoggingInitializer()
    {
        lock (this.@lock)
        {
            this.configuration = new ConfigurationBuilder().Build();
            this.isConfigurationBuilt = false;
        }
    }

    public ContainerLoggingInitializer(IConfiguration configuration)
    {
        lock (this.@lock)
        {
            this.configuration = configuration;
            this.isConfigurationBuilt = true;
        }
    }

    protected virtual IConfiguration BuildInternalConfiguration()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }

    public virtual ILoggerFactory Initialize()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            if (!this.isConfigurationBuilt)
            {
                this.configuration = this.BuildInternalConfiguration();
                this.isConfigurationBuilt = true;
            }

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConfiguration(this.configuration.GetSection("Logging")).AddConsole());
            return loggerFactory;
        }
    }
}
