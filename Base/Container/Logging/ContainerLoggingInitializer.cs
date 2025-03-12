using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Base.Container.Logging.Interfaces;

namespace Reoria.Engine.Base.Container.Logging;

public class ContainerLoggingInitializer : Disposable, IContainerLoggingInitializer
{
    protected readonly IConfiguration configuration;

    public ContainerLoggingInitializer() => this.configuration = this.BuildEarlyConfiguration();
    public ContainerLoggingInitializer(IConfiguration configuration) => this.configuration = configuration;

    protected virtual IConfiguration BuildEarlyConfiguration()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return builder.Build();
    }

    public virtual ILoggerFactory Initialize()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConfiguration(this.configuration.GetSection("Logging")).AddConsole());
        return loggerFactory;
    }
}
