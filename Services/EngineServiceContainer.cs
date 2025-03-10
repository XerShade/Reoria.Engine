using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Services.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Services;

/// <summary>
/// A service container that is responsible for managing and registering services in the engine's dependency injection container.
/// It handles the initialization, loading, adding, and configuring of services through loaders and a service provider.
/// </summary>
public class EngineServiceContainer : Disposable, IEngineServiceContainer
{
    /// <summary>
    /// The logger used for logging messages related to the service container's lifecycle and operations.
    /// </summary>
    protected readonly ILogger<IEngineServiceContainer> logger;
    /// <summary>
    /// The configuration instance used to configure the services within the container.
    /// This contains settings such as application configuration values that might be injected into services.
    /// </summary>
    protected readonly IConfiguration configuration;
    /// <summary>
    /// A collection of <see cref="Type"/>s that have been discovered by the engine as services to be registered and configured.
    /// </summary>
    /// <remarks>These services are discovered dynamically from assemblies that have classes that been tagged with the <see cref="ServiceAttribute"/> attribute.</remarks>
    protected readonly List<Type> serviceTypes;

    /// <summary>
    /// The collection of services registered within the container.
    /// </summary>
    public ServiceCollection Services { get; private set; }
    /// <summary>
    /// The service provider created from the service collection.
    /// </summary>
    public ServiceProvider Provider { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineServiceContainer"/> class with the provided logger factory and configuration.
    /// </summary>
    /// <param name="loggerFactory">The logger factory used to create loggers.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="loggerFactory"/> or <paramref name="configuration"/> is null.</exception>
    public EngineServiceContainer(ILoggerFactory loggerFactory)
    {
        // Ensure thread-safe initialization by acquiring the lock.
        lock (this.@lock)
        {
            // Create logger for the service container
            this.logger = loggerFactory.CreateLogger<IEngineServiceContainer>()
                ?? throw new ArgumentNullException(nameof(loggerFactory), "LoggerFactory cannot be null.");

            // Store the provided configuration
            this.configuration = this.DiscoverConfigurationFiles().Build();

            // Initialize the service type collection.
            this.serviceTypes = [];

            // Log the start of the service container construction
            this.logger.LogInformation("Constructing the service container.");

            try
            {
                // Create a new ServiceCollection for registering services
                this.Services = new ServiceCollection();

                // Build an empty service provider (this is a placeholder until services are registered)
                this.Provider = new ServiceCollection().BuildServiceProvider();

                // Log the registration of essential services
                this.logger.LogInformation("Registering essential services with the service container.");

                // Register essential services: Configuration, LoggerFactory, and Logger<>
                _ = this.Services.AddSingleton<IConfiguration>(this.configuration);
                _ = this.Services.AddSingleton<ILoggerFactory>(loggerFactory);
                _ = this.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

                // Log successful registration
                this.logger.LogInformation("Essential services registered with the service container successfully.");
            }
            catch (Exception ex)
            {
                // Log and rethrow exceptions during initialization
                this.logger.LogError(ex, "An error occurred during service container initialization.");
                throw;
            }
        }
    }

    protected IConfigurationBuilder DiscoverConfigurationFiles()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            // Create a new configuration builder instance.
            IConfigurationBuilder builder = new ConfigurationBuilder();

            try
            {
                // Get all assemblies loaded in the current application domain.
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                List<Type> serviceTypes = this.GetServiceAttributedClasses().ToList();

                // Iterate over all registered services and add them to the container.
                foreach (Type serviceType in serviceTypes)
                {
                    // Iterate over all static methods tagged with ServiceAttribute.RegisterConfigurationFiles.
                    foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        // Validate the parameters of the method.
                        if (method.GetCustomAttribute<ServiceAttribute.RegisterConfigurationFiles>() != null &&
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(IConfigurationBuilder))
                        {
                            // Invoke the method to add configuration files to the container.
                            logger.LogDebug("Invoking tagged configuration file method '{Method}' in '{Type}'.", method.Name, serviceType.FullName);
                            _ = method.Invoke(null, [builder]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and rethrow any errors encountered
                this.logger.LogError(ex, "An error occurred while discovering configuration files.");
                throw;
            }

            // Return the configuration builder.
            return builder;
        }
    }

    /// <summary>
    /// Discovers all service classes that have been tagged with the <see cref="ServiceAttribute"/> attribute.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    public virtual IEngineServiceContainer DiscoverServices()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // Get all assemblies loaded in the current application domain.
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                List<Type> serviceTypes = this.GetServiceAttributedClasses().ToList();

                // Log the number of services found and the number of assemblies scanned.
                this.logger.LogInformation("Found {ServiceCount} services across {AssemblyCount} assemblies.", serviceTypes.Count, assemblies.Length);

                // Register service types with the container.
                foreach (Type serviceType in serviceTypes)
                {
                    this.serviceTypes.Add(serviceType);
                    this.logger.LogDebug("Registered service '{ServiceType}' with the service container.", serviceType.FullName);
                }
            }
            catch (Exception ex)
            {
                // Log and rethrow any errors encountered
                this.logger.LogError(ex, "An error occurred while discovering services.");
                throw;
            }
        }

        // Return the current instance to support method chaining
        return this;
    }

    /// <summary>
    /// Registers services with the container by calling any static methods tagged with the <see cref="ServiceAttribute.RegisterServicesAttribute"/> attribute in the service.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    public virtual IEngineServiceContainer RegisterServices()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // If no services are available, log a warning and return early
                if (this.serviceTypes.Count == 0)
                {
                    this.logger.LogWarning("No services have been discovered by the engine.");
                    return this;
                }

                // Iterate over all registered services and add them to the container.
                foreach (Type serviceType in this.serviceTypes)
                {
                    // Iterate over all static methods tagged with ServiceAttribute.RegisterServicesAttribute.
                    foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        // Validate the parameters of the method.
                        if (method.GetCustomAttribute<ServiceAttribute.RegisterServicesAttribute>() != null &&
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(IServiceCollection))
                        {
                            // Invoke the method to add services to the container.
                            logger.LogDebug("Invoking tagged registration method '{Method}' in '{Type}'.", method.Name, serviceType.FullName);
                            _ = method.Invoke(null, [this.Services]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and rethrow any errors encountered while adding services
                this.logger.LogError(ex, "An error occurred while adding services.");
                throw;
            }
        }

        // Return the current instance to support method chaining
        return this;
    }

    /// <summary>
    /// Configures any services registerd with the container by calling any static methods tagged with the <see cref="ServiceAttribute.ConfigureServicesAttribute"/> attribute in the service.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    public virtual IEngineServiceContainer ConfigureServices()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // If no services are available, log a warning and return early
                if (this.serviceTypes.Count == 0)
                {
                    this.logger.LogWarning("No services have been discovered by the engine.");
                    return this;
                }

                // Iterate over all registered services and add them to the container.
                foreach (Type serviceType in this.serviceTypes)
                {
                    // Iterate over all static methods tagged with ServiceAttribute.ConfigureServicesAttribute.
                    foreach (MethodInfo method in serviceType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        // Validate the parameters of the method.
                        if (method.GetCustomAttribute<ServiceAttribute.ConfigureServicesAttribute>() != null &&
                            method.GetParameters().Length == 1 &&
                            method.GetParameters()[0].ParameterType == typeof(IServiceProvider))
                        {
                            // Invoke the method to configure services in the container.
                            logger.LogDebug("Invoking tagged configuration method '{Method}' in '{Type}'.", method.Name, serviceType.FullName);
                            _ = method.Invoke(null, [this.Provider]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and rethrow any errors encountered while configuring services
                this.logger.LogError(ex, "An error occurred while configuring services.");
                throw;
            }
        }

        // Return the current instance to support method chaining
        return this;
    }

    /// <summary>
    /// Builds the <see cref="ServiceProvider"/> from the registered services. Once this method is called, no more services can be added to the container.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service collection is empty when attempting to build the provider.</exception>
    public virtual IEngineServiceContainer BuildServiceProvider()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // If no services have been registered, log a warning
                if (this.Services.Count == 0)
                {
                    this.logger.LogWarning("No services were registered before building the service provider.");
                }

                // Log the start of building the service provider
                this.logger.LogInformation("Building the service provider. No more services can be added after this point.");

                // Build the service provider
                this.Provider = this.Services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                // Log and rethrow any errors encountered during provider building
                this.logger.LogError(ex, "An error occurred while building the service provider.");
                throw;
            }
        }

        // Return the current instance to support method chaining
        return this;
    }

    /// <summary>
    /// Finds all types in loaded assemblies that are decorated with [ServiceAttribute].
    /// </summary>
    private IEnumerable<Type> GetServiceAttributedClasses()
    {
        // Find all of the types that are tagged with ServiceAttribute across available assemblies.
        this.logger.LogInformation("Finding service loaders in available assemblies.");
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetCustomAttribute<ServiceAttribute>() != null);
    }
}
