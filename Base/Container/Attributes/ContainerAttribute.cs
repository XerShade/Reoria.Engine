namespace Reoria.Engine.Base.Container.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ContainerAttribute : Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DiscoverConfigurationSources : Attribute { }
}
