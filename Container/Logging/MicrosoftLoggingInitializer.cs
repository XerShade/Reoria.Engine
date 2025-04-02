using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Logging.Interfaces;

namespace Reoria.Engine.Container.Logging;

public class MicrosoftLoggingInitializer(IConfiguration configuration) : Disposable, ILoggingInitializer
{
    protected IConfiguration Configuration = configuration;

    public virtual ILoggerFactory CreateLoggerFactory()
    {
        lock (this.@lock)
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConfiguration(this.Configuration.GetSection("Logging")).AddConsole());
            return loggerFactory;
        }
    }
}
