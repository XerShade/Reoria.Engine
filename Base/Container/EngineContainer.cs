using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Configuration;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Container.Logging.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Base.Container;

public class EngineContainer<TLoggingInitalizer> : Disposable, IEngineContainer
    where TLoggingInitalizer : class, IContainerLoggingInitializer, new()
{
    protected ILogger<IEngineContainer> logger;
    protected IConfiguration configuration;
    protected IServiceCollection services;
    protected IServiceProvider provider;

    protected IEnumerable<Type> containerServiceClasses;
    protected ContainerConfigurationSources containerConfigurationSources;

    public EngineContainer()
    {
        using IContainerLoggingInitializer loggingInitializer = Activator.CreateInstance<TLoggingInitalizer>();
        using ILoggerFactory loggerFactory = loggingInitializer.Initialize();
        this.logger = loggerFactory.CreateLogger<IEngineContainer>() ?? throw new NullReferenceException();
        this.logger.LogInformation("Staring game engine container.");

        this.configuration = new ConfigurationBuilder().Build();
        this.services = new ServiceCollection();
        this.provider = this.services.BuildServiceProvider();

        this.containerServiceClasses = [];
        this.containerConfigurationSources = new(loggerFactory.CreateLogger<ContainerConfigurationSources>());
    }

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
        using IContainerLoggingInitializer loggingInitializer = 
            Activator.CreateInstance(typeof(TLoggingInitalizer), this.configuration) as IContainerLoggingInitializer ??
            throw new NullReferenceException("Unable to create container logging initializer.");

        this.logger.LogDebug("Initializing container logger factory.");
        using ILoggerFactory loggerFactory = loggingInitializer.Initialize();

        this.logger.LogDebug("Initializing container logger.");
        this.logger = loggerFactory.CreateLogger<IEngineContainer>();

        _ = this.services.AddSingleton<ILoggerFactory>(loggerFactory);
        _ = this.services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        this.logger.LogInformation("Successfully built container logger and logger factory.");

        return this;
    }
}
