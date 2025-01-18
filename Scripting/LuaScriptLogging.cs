using Microsoft.Extensions.Logging;
using Reoria.Engine.Scripting.Attributes;
using Reoria.Engine.Scripting.Interfaces;

namespace Reoria.Engine.Scripting;

public class LuaScriptLogging(ILogger<IScriptLogging> logger) : IScriptLogging
{
    protected readonly ILogger<IScriptLogging> logger = logger;

    [ScriptFunction("Logging.LogDebug")]
    public virtual void LogDebug(string message) => this.logger.LogDebug(message);

    [ScriptFunction("Logging.LogInformation")]
    public virtual void LogInformation(string message) => this.logger.LogInformation(message);

    [ScriptFunction("Logging.LogWarning")]
    public virtual void LogWarning(string message) => this.logger.LogWarning(message);

    [ScriptFunction("Logging.LogCritical")]
    public virtual void LogCritical(string message) => this.logger.LogCritical(message);

    [ScriptFunction("Logging.LogError")]
    public virtual void LogError(string message) => this.logger.LogError(message);

    [ScriptFunction("Logging.LogTrace")]
    public virtual void LogTrace(string message) => this.logger.LogTrace(message);
}
