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
    /// A collection of <see cref="IEngineServiceLoader"/> instances that are responsible for adding and configuring services.
    /// These loaders are discovered dynamically from assemblies and used to populate the container with services.
    /// </summary>
    protected readonly List<IEngineServiceLoader> loaders;

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
    /// <param name="configuration">The configuration used to configure services.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="loggerFactory"/> or <paramref name="configuration"/> is null.</exception>
    public EngineServiceContainer(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        // Ensure thread-safe initialization by acquiring the lock.
        lock (this.@lock)
        {
            // Create logger for the service container
            this.logger = loggerFactory.CreateLogger<IEngineServiceContainer>()
                ?? throw new ArgumentNullException(nameof(loggerFactory), "LoggerFactory cannot be null.");

            // Store the provided configuration
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null.");

            // Initialize the loaders list
            this.loaders = [];

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

    /// <summary>
    /// Finds and loads all available <see cref="IEngineServiceLoader"/> implementations from the assemblies in the current application domain.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    public virtual IEngineServiceContainer FindServiceLoaders()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // Get all assemblies loaded in the current application domain
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                this.logger.LogInformation("Finding service loaders in available assemblies.");

                // Find all types that implement IEngineServiceLoader and are not abstract or interfaces
                List<Type> serviceTypes = assemblies
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(IEngineServiceLoader).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                    .ToList();

                // Log the number of loaders found and the number of assemblies scanned
                this.logger.LogInformation("Found {ServiceLoaderCount} service loaders across {AssemblyCount} assemblies.", serviceTypes.Count, assemblies.Length);

                // Instantiate and add service loaders to the container
                foreach (Type serviceType in serviceTypes)
                {
                    if (Activator.CreateInstance(serviceType) is IEngineServiceLoader instance)
                    {
                        // Add the loaded service loader to the list of loaders
                        this.loaders.Add(instance);
                        this.logger.LogDebug("Added service loader '{LoaderType}' to the service container.", instance.GetType().FullName);
                    }
                    else
                    {
                        // Log a warning if an instance could not be created
                        this.logger.LogWarning("Failed to create an instance of service loader '{LoaderType}'.", serviceType.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and rethrow any errors encountered
                this.logger.LogError(ex, "An error occurred while finding service loaders.");
                throw;
            }
        }

        // Return the current instance to support method chaining
        return this;
    }

    /// <summary>
    /// Adds services to the container by calling the <see cref="IEngineServiceLoader.AddServices"/> method for each discovered loader.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    public virtual IEngineServiceContainer AddServices()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // If no loaders are available, log a warning and return early
                if (this.loaders.Count == 0)
                {
                    this.logger.LogWarning("No service loaders available to add services.");
                    return this;
                }

                // Iterate over all service loaders and use them to add services to the container
                foreach (IEngineServiceLoader serviceLoader in this.loaders)
                {
                    serviceLoader.AddServices(this.Services);
                    // Log the addition of services via the specific loader
                    this.logger.LogDebug("Added services using service loader '{LoaderType}'.", serviceLoader.GetType().FullName);
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
    /// Configures services in the container by calling the <see cref="IEngineServiceLoader.ConfigureServices"/> method for each discovered loader.
    /// This method is typically called after services have been added to the container.
    /// </summary>
    /// <returns>The current instance of the <see cref="IEngineServiceContainer"/> to allow method chaining.</returns>
    public virtual IEngineServiceContainer ConfigureServices()
    {
        // Ensure thread-safe operation by acquiring the lock.
        lock (this.@lock)
        {
            try
            {
                // If no loaders are available, log a warning and return early
                if (this.loaders.Count == 0)
                {
                    this.logger.LogWarning("No service loaders available to configure services.");
                    return this;
                }

                // Iterate over all service loaders and use them to configure services
                foreach (IEngineServiceLoader serviceLoader in this.loaders)
                {
                    serviceLoader.ConfigureServices(this.Provider);
                    // Log the configuration of services via the specific loader
                    this.logger.LogDebug("Configured services using service loader '{LoaderType}'.", serviceLoader.GetType().FullName);
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
}
