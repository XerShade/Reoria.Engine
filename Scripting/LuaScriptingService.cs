using Microsoft.Extensions.Logging;
using NLua;
using Reoria.Engine.Scripting.Interfaces;

namespace Reoria.Engine.Scripting;

public class LuaScriptingService : IScriptingService
{
    protected readonly ILogger<IScriptingService> logger;
    protected readonly Lua luaEngine;

    public LuaScriptingService(ILogger<IScriptingService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        this.logger = logger;
        this.luaEngine = new Lua();

        this.logger.LogInformation("Initalized {scriptingEngine}...", this.GetType().Name);
    }

    public void ExecuteScript(string scriptPath)
    {
        try
        {
            _ = this.luaEngine.DoFile(scriptPath);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error executing script {scriptPath}: {message}", scriptPath, ex.Message);
        }
    }

    public void RegisterFunction(string name, Delegate function)
    {
        try
        {
            this.luaEngine[name] = function;
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error registering function '{functionName}': {message}", function.Method.Name, ex.Message);
        }
    }

    public void LogInformation(string message) => this.logger.LogInformation("[LUA] " + message);
    public void LogDebug(string message) => this.logger.LogDebug("[LUA] " + message);
    public void LogWarning(string message) => this.logger.LogWarning("[LUA] " + message);
    public void LogError(string message) => this.logger.LogError("[LUA] " + message);
}
