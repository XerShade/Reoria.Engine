using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Logging.Interfaces;
using Reoria.Engine.Services.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Services;

public class EngineServiceContainer<TLoggingInitalizer> : Disposable, IEngineServiceContainer
    where TLoggingInitalizer : class, IEngineLoggingInitalizer, new()
{
    protected readonly ILoggerFactory loggerFactory;
    protected readonly ILogger<IEngineServiceContainer> logger;
    protected readonly IConfiguration configuration;
    protected readonly List<Type> serviceTypes;

    public ServiceCollection Services { get; private set; }
    public ServiceProvider Provider { get; private set; }

    public EngineServiceContainer()
    {
        lock (this.@lock)
        {
            using IEngineLoggingInitalizer loggingInitalizer = Activator.CreateInstance<TLoggingInitalizer>();
            this.loggerFactory = loggingInitalizer.Initialize();
            this.logger = this.loggerFactory.CreateLogger<IEngineServiceContainer>() ?? throw new NullReferenceException();
            this.logger.LogInformation("Starting game service container.");

            try
            {
                this.logger.LogInformation("Constructing service collection and provider.");
                this.Services = new ServiceCollection();
                this.Provider = new ServiceCollection().BuildServiceProvider();
                this.serviceTypes = [];

                this.logger.LogInformation("Constructing service container configuration.");
                this.configuration = this.DiscoverConfigurationFiles().Build();

                this.logger.LogInformation("Registering essential services with the service container.");
                _ = this.Services.AddSingleton<IConfiguration>(this.configuration);
                _ = this.Services.AddSingleton<ILoggerFactory>(this.loggerFactory);
                _ = this.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

                this.logger.LogInformation("Essential services registered with the service container successfully.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred during service container initialization.");
                throw;
            }
        }
    }

    private IEnumerable<Type> GetServiceAttributedClasses()
    {
        this.logger.LogInformation("Discovering service classes in available assemblies.");
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetCustomAttribute<ServiceAttribute>() != null);
    }

    protected void ExecuteFunctionsOnServices<TServiceAttribute>(List<Type> serviceTypes, params object[] parameters) where TServiceAttribute : Attribute
    {
        foreach (Type serviceType in serviceTypes)
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

                    if(isValidParameters)
                    {
                        logger.LogDebug("Invoking method '{Type}.{Method}'.", serviceType.FullName, method.Name);
                        _ = method.Invoke(null, parameters);
                    }
                }
            }
        }
    }

    public virtual IEngineServiceContainer DiscoverServices()
    {
        lock (this.@lock)
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                List<Type> serviceTypes = this.GetServiceAttributedClasses().ToList();

                this.logger.LogInformation("Discovered {ServiceCount} service classes across {AssemblyCount} assemblies.", serviceTypes.Count, assemblies.Length);

                foreach (Type serviceType in serviceTypes)
                {
                    this.serviceTypes.Add(serviceType);
                    this.logger.LogDebug("Discovered service class '{ServiceType}', tracking it with the service container.", serviceType.FullName);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while discovering services.");
                throw;
            }
        }

        return this;
    }

    protected IConfigurationBuilder DiscoverConfigurationFiles()
    {
        lock (this.@lock)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                List<Type> serviceTypes = this.GetServiceAttributedClasses().ToList();

                this.ExecuteFunctionsOnServices<ServiceAttribute.RegisterConfigurationFiles>(serviceTypes, [builder]);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while discovering configuration files.");
                throw;
            }

            return builder;
        }
    }

    public virtual IEngineServiceContainer RegisterServices()
    {
        lock (this.@lock)
        {
            try
            {
                if (this.serviceTypes.Count == 0)
                {
                    this.logger.LogWarning("No services have been discovered by the engine.");
                    return this;
                }

                this.ExecuteFunctionsOnServices<ServiceAttribute.RegisterServicesAttribute>(this.serviceTypes, [this.Services]);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while adding services.");
                throw;
            }
        }

        return this;
    }

    public virtual IEngineServiceContainer ConfigureServices()
    {
        lock (this.@lock)
        {
            try
            {
                if (this.serviceTypes.Count == 0)
                {
                    this.logger.LogWarning("No services have been discovered by the engine.");
                    return this;
                }

                this.ExecuteFunctionsOnServices<ServiceAttribute.ConfigureServicesAttribute>(this.serviceTypes, [this.Provider]);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while configuring services.");
                throw;
            }
        }

        return this;
    }

    public virtual IEngineServiceContainer BuildServiceProvider()
    {
        lock (this.@lock)
        {
            try
            {
                if (this.Services.Count == 0)
                {
                    this.logger.LogWarning("No services were registered before building the service provider.");
                }

                this.logger.LogInformation("Building the service provider. No more services can be added after this point.");

                this.Provider = this.Services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while building the service provider.");
                throw;
            }
        }

        return this;
    }
}
