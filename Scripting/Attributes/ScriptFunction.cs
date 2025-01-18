namespace Reoria.Engine.Scripting.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class ScriptFunction(string name) : Attribute
{
    public readonly string Name = name;
}
