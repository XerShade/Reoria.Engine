namespace Reoria.Engine.Container.Configuration;

public readonly struct EngineConfigurationSource(string path, bool optional, bool reloadOnChange)
{
    public readonly string Path = path ?? throw new ArgumentNullException(nameof(path));
    public readonly bool Optional = optional;
    public readonly bool ReloadOnChange = reloadOnChange;
}
