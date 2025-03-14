using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Configuration;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Container.Logging.Interfaces;
using Reoria.Engine.Base.Container.Services;
using System.Reflection;

namespace Reoria.Engine.Base.Container;

public class EngineContainer<TLoggingInitalizer> : Disposable, IEngineContainer
    where TLoggingInitalizer : class, IContainerLoggingInitializer
{
    protected ILogger<IEngineContainer> logger;
    protected IConfiguration configuration;
    protected IServiceCollection services;
    protected IServiceProvider provider;

    protected IEnumerable<Type> containerServiceClasses;
    protected ContainerConfigurationSources containerConfigurationSources;
    protected ContainerServiceDefinitions containerServiceDefinitions;

    public EngineContainer()
    {
        using IContainerLoggingInitializer loggingInitializer = this.CreateEarlyLoggingInitalizer<TLoggingInitalizer>();
        using ILoggerFactory loggerFactory = loggingInitializer.Initialize();
        this.logger = loggerFactory.CreateLogger<IEngineContainer>() ?? throw new NullReferenceException();
        this.logger.LogInformation("Staring game engine container.");

        this.configuration = new ConfigurationBuilder().Build();
        this.services = new ServiceCollection();
        this.provider = this.services.BuildServiceProvider();

        this.containerServiceClasses = [];
        this.containerConfigurationSources = new(loggerFactory.CreateLogger<ContainerConfigurationSources>());
        this.containerServiceDefinitions = new(loggerFactory.CreateLogger<ContainerServiceDefinitions>());
    }

    protected virtual IContainerLoggingInitializer CreateEarlyLoggingInitalizer<TInitalizer>()
        where TInitalizer : class, TLoggingInitalizer => Activator.CreateInstance<TInitalizer>() ??
            throw new NullReferenceException("Unable to create container logging initializer.");

    public virtual IEngineContainer DiscoverContainerServiceClasses()
    {
        this.logger.LogInformation("Discovering container services in available assemblies.");
        this.containerServiceClasses = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => type.GetCustomAttribute<ContainerAttribute>() != null).ToArray();
        this.logger.LogInformation("Discovered {ServiceCount} container services across {AssemblyCount} assemblies.", this.containerServiceClasses.Count(), AppDomain.CurrentDomain.GetAssemblies().Length);
        return this;
    }

    protected void ExecuteFunctionsOnServices<TServiceAttribute>(params object[] parameters) where TServiceAttribute : Attribute
    {
        foreach (Type serviceType in this.containerServiceClasses)
        {
            foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (method.GetCustomAttribute<TServiceAttribute>() != null &&
                    method.GetParameters().Length == parameters.Length)
                {
                    bool isValidParameters = true;
                    ParameterInfo[] methodParameters = method.GetParameters();
                    for (int i = 0; i < methodParameters.Length && isValidParameters; i++)
                    {
                        if (!parameters[i].GetType().IsAssignableTo(methodParameters[i].ParameterType))
                        {
                            logger.LogWarning("Invalid parameter '{Parameter}' of type '{ParameterType}' in method '{Type}.{Method}'.",
                                methodParameters[i].Name, methodParameters[i].ParameterType.Name, serviceType.FullName, method.Name);
                            isValidParameters = false;
                        }
                    }

                    if (isValidParameters)
                    {
                        logger.LogDebug("Invoking method '{Type}.{Method}'.", serviceType.FullName, method.Name);
                        _ = method.Invoke(null, parameters);
                    }
                }
            }
        }
    }

    public virtual IEngineContainer DiscoverConfigurationSources()
    {
        if (!this.containerServiceClasses.Any())
        {
            this.logger.LogWarning("No container services have been discovered by the engine.");
            return this;
        }

        this.logger.LogInformation("Discovering container configuration sources.");
        this.ExecuteFunctionsOnServices<ContainerAttribute.DiscoverConfigurationSources>(this.containerConfigurationSources);
        this.logger.LogInformation("Discovered {Count} container configuration sources.", this.containerConfigurationSources.Count());

        return this;
    }

    public virtual IEngineContainer BuildContainerConfiguration()
    {
        if (!this.containerConfigurationSources.Any())
        {
            this.logger.LogWarning("No container configuration sources have been discovered by the engine.");
            return this;
        }

        this.logger.LogInformation("Building container configuration using configuration sources.");

        IConfigurationBuilder builder = new ConfigurationBuilder();

        foreach(ContainerConfigurationSource source in this.containerConfigurationSources.GetSources())
        {
            this.logger.LogDebug("Building container configuration source '{Path}'.", source.Path);
            this.OnBuildContainerConfigurationSource(builder, source);
        }

        this.configuration = builder.Build();

        _ = this.services.AddSingleton<IConfiguration>(this.configuration);

        this.containerConfigurationSources.Dispose();

        this.logger.LogInformation("Successfully built container configuration.");

        return this;
    }

    protected virtual void OnBuildContainerConfigurationSource(IConfigurationBuilder builder, ContainerConfigurationSource source) =>
        _ = builder.AddJsonFile(source.Path, optional: source.Optional, reloadOnChange: source.ReloadOnChange);

    public virtual IEngineContainer BuildContainerLogger()
    {
        this.logger.LogInformation("Building container logger and logger factory.");

        this.logger.LogDebug("Initializing container logging initalizer.");
        using IContainerLoggingInitializer loggingInitializer = this.CreateLoggingInitalizer<TLoggingInitalizer>(this.configuration);

        this.logger.LogDebug("Initializing container logger factory.");
        ILoggerFactory loggerFactory = loggingInitializer.Initialize();

        this.logger.LogDebug("Initializing container logger.");
        this.logger = loggerFactory.CreateLogger<IEngineContainer>();

        _ = this.services.AddSingleton<ILoggerFactory>(loggerFactory);
        _ = this.services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        this.logger.LogInformation("Successfully built container logger and logger factory.");

        return this;
    }

    protected virtual IContainerLoggingInitializer CreateLoggingInitalizer<TInitalizer>(IConfiguration configuration)
        where TInitalizer : class, TLoggingInitalizer => Activator.CreateInstance(typeof(TInitalizer), configuration) as TInitalizer ??
            throw new NullReferenceException("Unable to create container logging initializer.");

    public virtual IEngineContainer DiscoverContainerServices()
    {
        if (!this.containerServiceClasses.Any())
        {
            this.logger.LogWarning("No container services have been discovered by the engine.");
            return this;
        }

        this.logger.LogInformation("Discovering container service definitions.");
        this.ExecuteFunctionsOnServices<ContainerAttribute.DiscoverSerivceDefinitions>(this.containerServiceDefinitions);
        this.logger.LogInformation("Discovered {Count} container service definitions.", this.containerServiceDefinitions.Count());

        return this;
    }

    public virtual IEngineContainer BuildContainerServices()
    {
        if (!this.containerServiceDefinitions.Any())
        {
            this.logger.LogWarning("No container service definitions have been discovered by the engine.");
            return this;
        }

        this.logger.LogInformation("Resolving container services using service definitions.");

        foreach (ContainerServiceDefinition service in this.containerServiceDefinitions.GetSources())
        {
            this.logger.LogDebug("Resolving container serivce '{Service}'.", service.Service.Name);
            _ = service.Lifetime switch
            {
                ServiceLifetime.Singleton => service.Instance is null
                                        ? this.services.AddSingleton(service.Service, service.Implementation)
                                        : this.services.AddSingleton(service.Service, service.Instance),
                ServiceLifetime.Transient => this.services.AddScoped(service.Service, service.Implementation),
                ServiceLifetime.Scoped => this.services.AddTransient(service.Service, service.Implementation),
                _ => this.services.AddTransient(service.Service, service.Implementation),
            };
        }

        this.containerServiceDefinitions.Dispose();

        this.logger.LogInformation("Successfully resolved container services.");

        return this;
    }

    public virtual IEngineContainer BuildContainerServiceProvider()
    {
        if (!this.containerServiceClasses.Any())
        {
            this.logger.LogWarning("No container services have been discovered by the engine.");
            return this;
        }

        this.logger.LogInformation("Building container service provider.");
        this.provider = this.services.BuildServiceProvider();
        this.ExecuteFunctionsOnServices<ContainerAttribute.BuildServiceProvider>(this.provider);

        return this;
    }

    public virtual TService RetrieveService<TService>() where TService : class
    {
        this.logger.LogInformation("Retrieving service '{Service}' from the container.", typeof(TService).Name);
        return this.provider.GetRequiredService<TService>();
    }
}
