using Microsoft.Extensions.Configuration;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Configuration.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Container.Configuration;

public class EngineConfigurationProvider : Disposable, IEngineConfigurationProvider
{
    protected readonly IConfigurationBuilder Builder;

    public virtual string Environment { get; } = string.Empty;
    public virtual string Version { get; } = string.Empty;
    public virtual bool EnvironmentVariables { get; } = true;
    public virtual bool UserSecrets { get; } = true;

    public EngineConfigurationProvider()
    {
        this.Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        this.Version = Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "1.0.0.0";

        this.Builder = new ConfigurationBuilder().SetBasePath(this.GetCurrentDirectory());
    }

    public virtual IConfigurationBuilder GetConfigurationBuilder()
    {
        _ = this.AddEnvironmentVariables();
        _ = this.AddUserSecrets(Assembly.GetExecutingAssembly());

        return this.Builder;
    }

    protected virtual string GetCurrentDirectory()
        => Directory.GetCurrentDirectory();

    public virtual IConfigurationBuilder AddJsonFile(string path, bool optional, bool reloadOnChange)
        => this.Builder.AddJsonFile(path, optional: optional, reloadOnChange: reloadOnChange);

    public virtual IConfigurationBuilder AddEnvironmentJsonFile(string path, bool reloadOnChange)
        => this.AddJsonFile(path.Replace(".json", $".{this.Environment}.json".ToLower()), true, reloadOnChange);

    protected virtual IConfigurationBuilder AddEnvironmentVariables()
    {
        ArgumentNullException.ThrowIfNull(this.Builder);

        if (this.EnvironmentVariables)
        {
            _ = this.Builder.AddEnvironmentVariables();
        }

        return this.Builder;
    }

    protected virtual IConfigurationBuilder AddUserSecrets(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(this.Builder);
        ArgumentNullException.ThrowIfNull(assembly);

        if (this.EnvironmentVariables)
        {
            _ = this.Builder.AddUserSecrets(assembly);
        }

        return this.Builder;
    }
}
