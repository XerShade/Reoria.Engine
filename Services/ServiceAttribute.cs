namespace Reoria.Engine.Services;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute : Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RegisterServicesAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ConfigureServicesAttribute : Attribute { }
}