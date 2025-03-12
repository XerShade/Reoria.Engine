namespace Reoria.Engine.Base.Container.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ContainerAttribute : Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DiscoverConfigurationSources : Attribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DiscoverSerivceDefinitions : Attribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BuildServiceProvider : Attribute { }
}
