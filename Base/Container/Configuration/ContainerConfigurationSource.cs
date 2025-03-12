namespace Reoria.Engine.Base.Container.Configuration;

public readonly struct ContainerConfigurationSource(string path, bool optional = false, bool reloadOnChange = false)
{
    public readonly string Path = path ?? throw new ArgumentNullException(nameof(path));
    public readonly bool Optional = optional;
    public readonly bool ReloadOnChange = reloadOnChange;
}
