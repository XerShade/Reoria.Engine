using Microsoft.Extensions.Logging;

namespace Reoria.Engine.Base.Container.Configuration;

public class ContainerConfigurationSources(ILogger<ContainerConfigurationSources> logger)
{
    protected readonly Lock @lock = new();
    protected readonly List<ContainerConfigurationSource> sources = [];
    protected readonly ILogger<ContainerConfigurationSources> logger = logger;

    public bool Any()
    {
        lock (this.@lock)
        {
            return this.sources.Count != 0;
        }
    }

    public int Count()
    {
        lock (this.@lock)
        {
            return this.sources.Count;
        }
    }

    public ContainerConfigurationSource[] GetSources()
    {
        lock (this.@lock)
        {
            return [.. this.sources];
        }
    }

    public virtual void Dispose()
    {
        lock (this.@lock)
        {
            this.sources.Clear();
        }
    }

    public virtual void Add(string path, bool optional = false, bool reloadOnChange = false)
    {
        lock (this.@lock)
        {
            bool source = (from s in this.sources
                           where s.Path.Equals(path)
                           select s).Any();

            if (!source)
            {
                this.sources.Add(new ContainerConfigurationSource(path, optional, reloadOnChange));
                this.logger.LogDebug("Added configuration source '{Path}', Optional: {Optional}, ReloadOnChange: {ReloadOnChange}",
                    path, optional, reloadOnChange);
            }
        }
    }

    public virtual void Remove(string path)
    {
        lock (this.@lock)
        {
            ContainerConfigurationSource source = (from s in this.sources
                                                   where s.Path.Equals(path)
                                                   select s).FirstOrDefault();

            if (this.sources.Remove(source))
            {
                this.logger.LogDebug("Removed configuration source '{Path}'.", path);
            }
        }
    }
}
