
using System.Reflection;

namespace Reoria.Engine.Scripting.Interfaces;

public interface IScriptingService
{
    void ExecuteFunction(string functionName, params object[] args);
    void ExecuteScript(string scriptPath);
    void RegisterFunction(string name, Delegate function);
    void RegisterFunctions(Assembly assembly);
}
