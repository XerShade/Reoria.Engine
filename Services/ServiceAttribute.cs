namespace Reoria.Engine.Services;

/// <summary>
/// This attribute is used to mark a class as a service within the application.
/// It is typically applied to classes that provide core functionality, business logic,
/// or services that need to be injected or accessed throughout the system.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute : Attribute
{
    /// <summary>
    /// This attribute is used to mark methods within a service class that are responsible
    /// for registering additional services into the application's service container.
    /// These methods can be used to configure dependencies and register them for use throughout
    /// the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RegisterServicesAttribute : Attribute { }

    /// <summary>
    /// This attribute is used to mark methods within a service class that are responsible
    /// for configuring the services. These methods typically configure service-specific settings,
    /// options, or environment setup for the application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ConfigureServicesAttribute : Attribute { }
}
