using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Scripting.Interfaces;
using Reoria.Engine.Services.Interfaces;

namespace Reoria.Engine.Scripting.ServiceLoaders;

public class LuaServiceLoader : IEngineServiceLoader
{
    public void AddServices(IServiceCollection services) => services.AddSingleton<IScriptingService, LuaScriptingService>();

    public void ConfigureServices(IServiceProvider provider)
    {
        LuaScriptingService lua = provider.GetRequiredService<IScriptingService>() as LuaScriptingService ?? 
            throw new NullReferenceException("Unable to acquire the LuaScriptingService instance.");
        lua.RegisterFunction("LogInformation", lua.LogInformation);
        lua.RegisterFunction("LogWarning", lua.LogWarning);
        lua.RegisterFunction("LogError", lua.LogError);
        lua.RegisterFunction("LogDebug", lua.LogDebug);
    }
}
