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

    protected IConfigurationBuilder DiscoverConfigurationFiles()
    {
        lock (this.@lock)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                List<Type> serviceTypes = this.GetServiceAttributedClasses().ToList();

                foreach (Type serviceType in serviceTypes)
                {
                    foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (method.GetCustomAttribute<ServiceAttribute.RegisterConfigurationFiles>() != null &&
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(IConfigurationBuilder))
                        {
                            logger.LogDebug("Invoking tagged configuration file method '{Method}' in '{Type}'.", method.Name, serviceType.FullName);
                            _ = method.Invoke(null, [builder]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while discovering configuration files.");
                throw;
            }

            return builder;
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

                this.logger.LogInformation("Found {ServiceCount} services across {AssemblyCount} assemblies.", serviceTypes.Count, assemblies.Length);

                foreach (Type serviceType in serviceTypes)
                {
                    this.serviceTypes.Add(serviceType);
                    this.logger.LogDebug("Registered service '{ServiceType}' with the service container.", serviceType.FullName);
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

                foreach (Type serviceType in this.serviceTypes)
                {
                    foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (method.GetCustomAttribute<ServiceAttribute.RegisterServicesAttribute>() != null &&
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(IServiceCollection))
                        {
                            logger.LogDebug("Invoking tagged registration method '{Method}' in '{Type}'.", method.Name, serviceType.FullName);
                            _ = method.Invoke(null, [this.Services]);
                        }
                    }
                }
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

                foreach (Type serviceType in this.serviceTypes)
                {
                    foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (method.GetCustomAttribute<ServiceAttribute.ConfigureServicesAttribute>() != null &&
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(IServiceProvider))
                        {
                            logger.LogDebug("Invoking tagged configuration method '{Method}' in '{Type}'.", method.Name, serviceType.FullName);
                            _ = method.Invoke(null, [this.Provider]);
                        }
                    }
                }
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

    private IEnumerable<Type> GetServiceAttributedClasses()
    {
        this.logger.LogInformation("Finding service loaders in available assemblies.");
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetCustomAttribute<ServiceAttribute>() != null);
    }
}
