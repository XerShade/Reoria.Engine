using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Container;

public abstract class EngineContainer<TLoggingInitalizer> : Disposable, IEngineContainer
    where TLoggingInitalizer : class, ILoggingInitializer
{
    public ILogger<IEngineContainer> Logger { get; protected set; }
    public IConfiguration Configuration { get; protected set; }
    public IServiceCollection Services { get; protected set; }
    public IServiceProvider Provider { get; protected set; }

    public EngineContainer()
    {
        this.Services = new ServiceCollection();
        this.Provider = this.Services.BuildServiceProvider();
        this.Configuration = this.CreateEarlyConfigurationBuilder(new ConfigurationBuilder()).Build();
        this.Logger = this.CreateEarlyLogger();
        this.ReportContainerConstruction();
    }

    protected virtual ILoggingInitializer CreateEarlyLoggingInitalizer<TInitalizer>()
        where TInitalizer : class, TLoggingInitalizer
        => Activator.CreateInstance(typeof(TInitalizer), this.Configuration) as TInitalizer
        ?? throw new NullReferenceException("Unable to create logging initializer.");

    protected virtual ILoggerFactory CreateEarlyLoggingFactory(ILoggingInitializer loggingInitializer)
        => loggingInitializer.CreateLoggerFactory()
        ?? throw new NullReferenceException("Unable to create logger factory.");

    protected virtual ILogger<IEngineContainer> CreateEarlyLogger()
    {
        using ILoggingInitializer loggingInitializer = this.CreateEarlyLoggingInitalizer<TLoggingInitalizer>();
        using ILoggerFactory loggerFactory = this.CreateEarlyLoggingFactory(loggingInitializer);
        return loggerFactory.CreateLogger<IEngineContainer>();
    }

    protected virtual IConfigurationBuilder CreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
    {
        return builder
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly());
    }

    protected virtual void ReportContainerConstruction()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        if (assembly != null)
        {
            this.Logger.LogInformation("Creating the dependency injection container for the game, please wait." +
                "\n    - Application: '{ApplicationExe}', Version: '{ApplicationVersion}'",
                assembly.GetName().Name, assembly.GetName().Version);
        }
    }

    public virtual IEngineContainer CreateConfiguration()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container configuration.");
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            this.OnCreateConfiguration(builder);

            this.Configuration = builder.Build();
        }

        return this;
    }

    protected virtual void OnCreateConfiguration(IConfigurationBuilder builder)
    {
        _ = builder.AddEnvironmentVariables();
        _ = builder.AddUserSecrets(Assembly.GetExecutingAssembly());
    }

    public virtual IEngineContainer CreateServiceCollection()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container logger and logger factory.");
            using ILoggingInitializer loggingInitializer = this.CreateLoggingInitalizer<TLoggingInitalizer>(this.Configuration)
                ?? throw new NullReferenceException("Unable to create logging initializer.");
            ILoggerFactory loggerFactory = loggingInitializer.CreateLoggerFactory()
                ?? throw new NullReferenceException("Unable to create logger factory.");
            this.Logger = loggerFactory.CreateLogger<IEngineContainer>()
                ?? throw new NullReferenceException("Unable to logger.");

            this.Logger.LogInformation("Creating dependency injection container service collection.");
            _ = this.Services.AddSingleton<ILoggerFactory>(loggerFactory);
            _ = this.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            _ = this.Services.AddSingleton<IConfiguration>(this.Configuration);

            this.OnCreateServiceCollection(this.Services);
        }

        return this;
    }

    protected virtual ILoggingInitializer CreateLoggingInitalizer<TInitalizer>(IConfiguration configuration)
        where TInitalizer : class, TLoggingInitalizer
        => Activator.CreateInstance(typeof(TInitalizer), configuration) as TInitalizer
        ?? throw new NullReferenceException("Unable to create logging initializer.");

    protected virtual void OnCreateServiceCollection(IServiceCollection services) { }

    public virtual IEngineContainer CreateServiceProvider()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container service provider.");
            this.Provider = this.Services.BuildServiceProvider();
            this.OnCreateServiceProvider(this.Provider);
        }

        return this;
    }

    protected virtual void OnCreateServiceProvider(IServiceProvider provider) { }
}