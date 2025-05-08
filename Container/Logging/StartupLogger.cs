using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Logging.Interfaces;

namespace Reoria.Engine.Container.Logging;

public class StartupLogger
{
    protected readonly ILoggerFactory LoggerFactory;

    public StartupLogger(IServiceProvider serviceProvider)
    {
        
    }

    public virtual ILogger<TLoggerType> GetLogger<TLoggerType>() where TLoggerType:ILogger
    {
        using ILoggingInitializer loggingInitializer = this.Provider.GetRequiredService<ILoggingInitializer>();
        using ILoggerFactory loggerFactory = loggingInitializer.CreateLoggerFactory();
        return loggerFactory.CreateLogger<IEngineContainer>();
    }
}
