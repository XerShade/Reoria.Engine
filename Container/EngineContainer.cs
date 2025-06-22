using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Common.Security.Cryptography.Factories;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Container.Logging.Interfaces;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;
using Reoria.Engine.Signals;
using Reoria.Engine.Signals.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Container;

public abstract class EngineContainer : Disposable, IEngineContainer
{
    public ILogger<IEngineContainer> Logger { get; protected set; }
    public ILoggerFactory LoggerFactory { get; protected set; }
    public IConfiguration Configuration { get; protected set; }
    public IServiceCollection Services { get; protected set; }
    public IServiceProvider Provider { get; protected set; }

    public EngineContainer(IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        this.Logger = new StartupLoggerFactory(serviceProvider).GetLogger<IEngineContainer>();
        this.ReportContainerConstruction();

        this.Configuration = this.CreateConfiguration(serviceProvider.GetRequiredService<IEngineConfigurationSources>());
        this.LoggerFactory = this.CreateLoggerFactory(serviceProvider.GetRequiredService<IEngineLoggerFactory>());
        this.Logger = this.CreateLogger(this.LoggerFactory);
        this.Services = this.CreateServiceCollection(services);
        this.Provider = this.CreateServiceProvider();
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

    protected virtual IConfiguration CreateConfiguration(IEngineConfigurationSources configurationSources)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container configuration.");

            return configurationSources.GetConfiguration();
        }
    }

    protected virtual ILoggerFactory CreateLoggerFactory(IEngineLoggerFactory engineLoggerFactory)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container logger factory.");

            return engineLoggerFactory.GetLoggerFactory();
        }
    }

    protected virtual ILogger<IEngineContainer> CreateLogger(ILoggerFactory loggerFactory)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container logger.");

            return loggerFactory.CreateLogger<IEngineContainer>();
        }
    }

    protected virtual IServiceCollection CreateServiceCollection(IServiceCollection services)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container service collection.");

            _ = services.AddSingleton<IConfiguration>(this.Configuration);
            _ = services.AddSingleton<ILoggerFactory>(this.LoggerFactory);
            _ = services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            this.OnCreateServiceCollection(services);

            return services;
        }
    }

    protected virtual IServiceProvider CreateServiceProvider()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container service provider.");
            IServiceProvider provider = this.Services.BuildServiceProvider();

            this.Logger.LogInformation("Configuring dependency injection container services.");
            this.OnConfigureServices(provider);

            return provider;
        }
    }

    protected virtual void OnCreateServiceCollection(IServiceCollection services)
    {
        _ = services.AddScoped<IHashGenerator, HashGenerator>();
        _ = services.AddScoped<ISaltGenerator, SaltGenerator>();
        _ = services.AddSingleton<ISignalBus, SignalBus>();
    }

    protected virtual void OnConfigureServices(IServiceProvider provider)
    {
        HashGeneratorFactory.SetServiceProvider(provider);
        SaltGeneratorFactory.SetServiceProvider(provider);
    }
}