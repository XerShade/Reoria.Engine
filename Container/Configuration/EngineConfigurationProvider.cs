using Microsoft.Extensions.Configuration;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Configuration.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Container.Configuration;

public abstract class EngineConfigurationProvider : Disposable, IEngineConfigurationProvider
{
    public virtual string Environment { get; } = string.Empty;
    public virtual string Version { get; } = string.Empty;
    public virtual bool EnvironmentVariables { get; } = true;
    public virtual bool UserSecrets { get; } = true;

    public EngineConfigurationProvider()
    {
        this.Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        this.Version = Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "1.0.0.0";
    }

    public virtual IConfigurationBuilder CreateEarlyConfigurationBuilder()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        
        this.OnSetEarlyConfigurationBuilderBasePath(builder);
        this.OnCreateEarlyConfigurationBuilder(builder);

        if(this.EnvironmentVariables)
        {
            _ = builder.AddEnvironmentVariables();
        }

        if (this.UserSecrets)
        {
            _ = builder.AddUserSecrets(Assembly.GetExecutingAssembly());
        }

        return builder;
    }

    protected virtual void OnSetEarlyConfigurationBuilderBasePath(IConfigurationBuilder builder)
        => builder.SetBasePath(Directory.GetCurrentDirectory());

    protected abstract void OnCreateEarlyConfigurationBuilder(IConfigurationBuilder builder);

    public virtual IConfigurationBuilder CreateConfigurationBuilder()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();

        this.OnSetConfigurationBuilderBasePath(builder);
        this.OnCreateConfigurationBuilder(builder);

        if (this.EnvironmentVariables)
        {
            _ = builder.AddEnvironmentVariables();
        }

        if (this.UserSecrets)
        {
            _ = builder.AddUserSecrets(Assembly.GetExecutingAssembly());
        }

        return builder;
    }

    protected virtual void OnSetConfigurationBuilderBasePath(IConfigurationBuilder builder)
        => builder.SetBasePath(Directory.GetCurrentDirectory());

    protected abstract void OnCreateConfigurationBuilder(IConfigurationBuilder builder);
}
