using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Common.Security.Cryptography.Factories;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging.Interfaces;
using Reoria.Engine.Events;
using Reoria.Engine.Events.Interfaces;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Container;

public abstract class EngineContainer : Disposable, IEngineContainer
{
    public ILogger<IEngineContainer> Logger { get; protected set; }
    public IConfiguration Configuration { get; protected set; }
    public IServiceCollection Services { get; protected set; }
    public IServiceProvider Provider { get; protected set; }

    public EngineContainer(IServiceCollection services)
    {
        this.Services = services;
        this.Provider = this.Services.BuildServiceProvider();

        this.Configuration = this.CreateEarlyConfigurationBuilder().Build();
        _ = this.Services.AddSingleton<IConfiguration>(this.Configuration);
        this.Provider = this.Services.BuildServiceProvider();

        this.Logger = this.CreateEarlyLogger();

        this.Services = services;
        this.Provider = this.Services.BuildServiceProvider();
        this.ReportContainerConstruction();
    }

    protected virtual IConfigurationBuilder CreateEarlyConfigurationBuilder()
    {
        IEngineConfigurationProvider provider = this.Provider.GetRequiredService<IEngineConfigurationProvider>();
        return provider.CreateEarlyConfigurationBuilder();
    }

    protected virtual ILogger<IEngineContainer> CreateEarlyLogger()
    { 
        using ILoggingInitializer loggingInitializer = this.Provider.GetRequiredService<ILoggingInitializer>();
        using ILoggerFactory loggerFactory = loggingInitializer.CreateLoggerFactory();
        return loggerFactory.CreateLogger<IEngineContainer>();
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

            IEngineConfigurationProvider provider = this.Provider.GetRequiredService<IEngineConfigurationProvider>();

            this.Configuration = provider.CreateConfigurationBuilder().Build();

            _ = this.Services.AddSingleton<IConfiguration>(this.Configuration);
        }

        return this;
    }

    public virtual IEngineContainer CreateServiceCollection()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container logger and logger factory.");
            using ILoggingInitializer loggingInitializer = this.Provider.GetRequiredService<ILoggingInitializer>()
                ?? throw new NullReferenceException("Unable to create logging initializer.");
            ILoggerFactory loggerFactory = loggingInitializer.CreateLoggerFactory()
                ?? throw new NullReferenceException("Unable to create logger factory.");
            this.Logger = loggerFactory.CreateLogger<IEngineContainer>()
                ?? throw new NullReferenceException("Unable to logger.");

            _ = this.Services.AddSingleton<ILoggerFactory>(loggerFactory);
            _ = this.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            this.OnCreateServiceCollection(this.Services);
        }

        return this;
    }

    protected virtual void OnCreateServiceCollection(IServiceCollection services)
    {
        this.Logger.LogInformation("Creating dependency injection container service collection.");

        _ = services.AddScoped<IHashGenerator, HashGenerator>();
        _ = services.AddScoped<ISaltGenerator, SaltGenerator>();

        _ = services.AddSingleton<IEventBus, EventBus>();
    }

    public virtual IEngineContainer CreateServiceProvider()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container service provider.");
            this.Provider = this.Services.BuildServiceProvider();

            this.Logger.LogInformation("Configuring dependency injection container services.");
            this.OnConfigureServices(this.Provider);
        }

        return this;
    }

    protected virtual void OnConfigureServices(IServiceProvider provider)
    {
        HashGeneratorFactory.SetServiceProvider(provider);
        SaltGeneratorFactory.SetServiceProvider(provider);
    }
}