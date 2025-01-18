namespace Reoria.Engine.Scripting.Interfaces;

public interface IScriptLogging
{
    void LogCritical(string message);
    void LogDebug(string message);
    void LogError(string message);
    void LogInformation(string message);
    void LogTrace(string message);
    void LogWarning(string message);
}
