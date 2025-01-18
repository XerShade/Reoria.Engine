using Microsoft.Extensions.Logging;
using NLua;
using Reoria.Engine.Scripting.Attributes;
using Reoria.Engine.Scripting.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Reoria.Engine.Scripting;

public class LuaScriptingService : IScriptingService
{
    protected static ILogger<IScriptingService> staticLogger; // Temporary code.
    protected readonly ILogger<IScriptingService> logger;
    protected readonly IServiceProvider serviceProvider;
    protected readonly IScriptLoader scriptLoader;
    protected readonly Lua luaEngine;

    public LuaScriptingService(ILogger<IScriptingService> logger, IScriptLoader scriptLoader, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(logger);

        this.logger = logger;
        staticLogger = logger; // Temporary code.
        this.scriptLoader = scriptLoader;
        this.serviceProvider = serviceProvider;
        this.luaEngine = new Lua();

        this.logger.LogInformation("Initalized {scriptingEngine}...", this.GetType().Name);
    }


    [ScriptFunction("ScriptingService.ExecuteScript")]
    public void ExecuteScript(string scriptPath)
    {
        try
        {
            using StreamReader streamReader = new(this.scriptLoader.OpenStream(scriptPath));
            string luaScript = streamReader.ReadToEnd();

            try
            {
                _ = this.luaEngine.DoString(luaScript);
            }
            catch (Exception luaEx)
            {
                // Log details about the Lua error.
                this.logger.LogError(luaEx, "Lua script execution error in {scriptPath}: {message}", scriptPath, luaEx.Message);

                // Re-throw the exception if needed for further handling.
                throw;
            }
        }
        catch (Exception ex)
        {
            // Log any file reading or general execution errors.
            this.logger.LogError(ex, "Error executing script {scriptPath}: {message}", scriptPath, ex.Message);
        }
    }

    [ScriptFunction("ScriptingService.RegisterFunction")]
    public void RegisterFunction(string fullName, Delegate function)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        try
        {
            if (!fullName.Contains("."))
            {
                this.luaEngine[fullName] = function;
                this.logger.LogDebug("Registered function {functionName} under namespace {namespace}.", fullName, fullName);
                return;
            }

            string[] parts = fullName.Split('.');
            LuaTable? currentTable = this.luaEngine["_G"] as LuaTable;
            if (currentTable == null)
            {
                this.luaEngine.NewTable("_G");
                currentTable = this.luaEngine["_G"] as LuaTable;
            }

            for (int i = 0; i < parts.Length - 1; i++)
            {
                string part = parts[i];
                if (currentTable[part] is not LuaTable)
                {
                    this.luaEngine.NewTable(part);
                    object newTable = this.luaEngine[part];
                    currentTable[part] = newTable;
                }
                currentTable = currentTable[part] as LuaTable;
            }

            string functionName = parts[^1];
            currentTable[functionName] = function;
            this.logger.LogDebug("Registered function {functionName} under namespace {namespace}.", functionName, fullName);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error registering function '{functionName}': {message}", function.Method.Name, ex.Message);
        }
    }

    [ScriptFunction("ScriptingService.RegisterFunctions")]
    public void RegisterFunctions(Assembly assembly)
    {
        // Scan the assembly for any methods that have a ScriptFunction attribute. 
        IEnumerable<MethodInfo> methods = assembly.GetTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            .Where(m => m.GetCustomAttribute<ScriptFunction>() != null);

        // Loop through and process all of the methods.
        foreach (MethodInfo method in methods)
        {
            // Verify that the method has a ScriptFunction and if not then just ignore it.
            ScriptFunction? function = method.GetCustomAttribute<ScriptFunction>();
            if (function == null)
            {
                continue;
            }

            // Add the function to the lua instance.
            this.RegisterFunction(function.Name, this.CreateLuaDelegate(method));
        }
    }

    protected virtual Delegate CreateLuaDelegate(MethodInfo methodInfo)
    {
        try
        {
            // Build a compatible delegate type dynamically based on the method parameters.
            Type[] parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();

            // The return type is always void since packet handlers return nothing.
            Type delegateType = Expression.GetActionType(parameterTypes);

            // Create the delegate.
            return methodInfo.IsStatic
                ? Delegate.CreateDelegate(delegateType, methodInfo)
                : Delegate.CreateDelegate(delegateType, this.ResolveServiceFromContainer(methodInfo.DeclaringType ??
                    throw new NullReferenceException($"Could not resolve an instance of {methodInfo.DeclaringType} from DI container.")) ??
                    throw new InvalidOperationException($"Could not resolve an instance of {methodInfo.DeclaringType} from DI container."),
                    methodInfo);
        }
        catch (Exception ex)
        {
            // Report the error to the logger for review.
            this.logger.LogError(ex, "Failed to create delegate for method {methodName}, reason: {reason}", methodInfo.Name, ex.Message);
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    protected virtual object? ResolveServiceFromContainer(Type? declaringType)
    {
        if (declaringType == null)
        {
            throw new NullReferenceException("DeclaringType is null.");
        }

        Type? interfaceType = declaringType.GetInterfaces()
            .FirstOrDefault(i => serviceProvider.GetService(i) != null);
        if (interfaceType != null)
        {
            return serviceProvider.GetService(interfaceType);
        }

        return serviceProvider.GetService(declaringType);
    }

    // Temporary code, will be removed later. Do not use aside from testing.
    [ScriptFunction("Logging.LogInformation")]
    public static void LogInformation(string message) => staticLogger.LogInformation(message);
}
