using Microsoft.Extensions.Configuration;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Configuration.Interfaces;

namespace Reoria.Engine.Container.Configuration;

public class EngineConfigurationSources(IEngineConfigurationProvider provider) : Disposable, IEngineConfigurationSources
{
    protected readonly List<EngineConfigurationSource> Sources = [];
    protected readonly IEngineConfigurationProvider Provider = provider;

    public virtual void AddSource(string path, bool optional, bool reloadOnChange)
        => this.Sources.Add(new(path, optional: optional, reloadOnChange: reloadOnChange));

    public virtual IConfiguration GetConfiguration()
    {
        lock(this.@lock)
        {
            foreach (EngineConfigurationSource source in this.Sources)
            {
                _ = this.Provider.AddJsonFile(source.Path, optional: source.Optional, reloadOnChange: source.ReloadOnChange);
            }

            foreach (EngineConfigurationSource source in this.Sources)
            {
                _ = this.Provider.AddEnvironmentJsonFile(source.Path, reloadOnChange: source.ReloadOnChange);
            }

            return this.Provider.GetConfigurationBuilder().Build();
        }
    }
}
