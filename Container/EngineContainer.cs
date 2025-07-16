using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Container.Logging.Interfaces;
using Reoria.Engine.Container.Registrars;
using System.Reflection;

namespace Reoria.Engine.Container;

public class EngineContainer : Disposable, IEngineContainer
{
    protected readonly ContainerBuilder ContainerBuilder;
    public ILogger<IEngineContainer> Logger { get; protected set; }
    public ILoggerFactory LoggerFactory { get; protected set; }
    public IConfiguration Configuration { get; protected set; }
    public IServiceProvider Provider { get; protected set; }

    public EngineContainer(IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        this.Logger = new StartupLoggerFactory(serviceProvider).GetLogger<IEngineContainer>();
        this.ReportContainerConstruction();

        this.Configuration = this.CreateConfiguration(serviceProvider.GetRequiredService<IEngineConfigurationSources>());
        this.LoggerFactory = this.CreateLoggerFactory(serviceProvider.GetRequiredService<IEngineLoggerFactory>());
        this.Logger = this.CreateLogger(this.LoggerFactory);
        this.ContainerBuilder = this.CreateServiceCollection(services);
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

    protected virtual IEnumerable<TRegistrar> GetRegistrars<TRegistrar>()
    {
        List<TRegistrar> registrars = [];
        IEnumerable<Type> registrarTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(TRegistrar).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();

        foreach (Type type in registrarTypes)
        {
            try
            {
                TRegistrar registrar = (TRegistrar)Activator.CreateInstance(type)!;
                registrars.Add(registrar);
                this.Logger.LogDebug("Created new instance of registrar '{RegistrarType}'.", type.Name);
            }
            catch (Exception ex)
            {
                this.Logger.LogWarning("Unable to load registrar of type '{RegistrarType}', reason: '{ErrorMessage}'", type.Name, ex.Message);
            }
        }

        this.Logger.LogInformation("Successfully loaded '{Count}' of '{TypeCount}' {RegistrarType} registrars.", registrars.Count, registrarTypes.Count(), typeof(TRegistrar).Name);

        return registrars.AsEnumerable();
    }

    protected virtual IConfiguration CreateConfiguration(IEngineConfigurationSources configurationSources)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container configuration.");

            IEnumerable<IConfigurationRegistrar> registrars = this.GetRegistrars<IConfigurationRegistrar>();

            foreach (IConfigurationRegistrar registrar in registrars)
            {
                registrar.RegisterSources(configurationSources);
            }

            return configurationSources.GetConfiguration();
        }
    }

    protected virtual ILoggerFactory CreateLoggerFactory(IEngineLoggerFactory engineLoggerFactory)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating dependency injection container logger factory.");

            return engineLoggerFactory.SetupFactory(this.Configuration).GetLoggerFactory();
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

    protected virtual ContainerBuilder CreateServiceCollection(IServiceCollection services)
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating Autofac-based dependency injection container.");

            ContainerBuilder builder = new();

            builder.Populate(services);

            _ = builder.RegisterInstance(this.Configuration)
                .As<IConfiguration>()
                .SingleInstance();

            _ = builder.RegisterInstance(this.LoggerFactory)
                .As<ILoggerFactory>()
                .SingleInstance();

            _ = builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();

            IEnumerable<IServiceRegistrar> registrars = this.GetRegistrars<IServiceRegistrar>();

            foreach (IServiceRegistrar registrar in registrars)
            {
                registrar.RegisterServices(builder, this.Configuration, this.LoggerFactory);
            }

            return builder;
        }
    }

    protected virtual IServiceProvider CreateServiceProvider()
    {
        lock (this.@lock)
        {
            this.Logger.LogInformation("Creating Autofac-based dependency injection container service provider.");

            IContainer container = this.ContainerBuilder.Build();
            IServiceProvider provider = new AutofacServiceProvider(container);

            this.Logger.LogInformation("Configuring Autofac-based dependency injection container service provider.");

            IEnumerable<IServiceConfigurationRegistrar> registrars = this.GetRegistrars<IServiceConfigurationRegistrar>();

            foreach (IServiceConfigurationRegistrar registrar in registrars)
            {
                registrar.ConfigureServices(provider);
            }

            return provider;
        }
    }
}