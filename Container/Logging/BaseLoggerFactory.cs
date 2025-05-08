using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Logging.Interfaces;

namespace Reoria.Engine.Container.Logging;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public abstract class BaseLoggerFactory : Disposable, IEngineLoggerFactory
{
    protected ILoggerFactory LoggerFactory;

    public virtual ILoggerFactory GetLoggerFactory()
        => this.LoggerFactory;

    public virtual ILogger<T> GetLogger<T>()
        => this.LoggerFactory.CreateLogger<T>();
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.