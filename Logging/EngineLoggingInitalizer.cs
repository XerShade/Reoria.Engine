using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Base.Common;
using Reoria.Engine.Logging.Interfaces;

namespace Reoria.Engine.Logging;

/// <summary>
/// This class is responsible for initializing an <see cref="ILoggerFactory"/> 
/// to be used for logging purposes before the engine is fully initialized 
/// and its Dependency Injection (DI) container and configuration is loaded.
/// It ensures that logging is available early in the application lifecycle.
/// </summary>
public class EngineLoggingInitalizer : Disposable, IEngineLoggingInitalizer
{
    /// <summary>
    /// Initializes an <see cref="ILoggerFactory"/> before the engine is fully initialized.
    /// This method configures the logger with settings from the "Logging" section of the app configuration 
    /// and enables console logging.
    /// </summary>
    /// <returns>An instance of <see cref="ILoggerFactory"/> for logging.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the object has already been disposed.</exception>
    public virtual ILoggerFactory Initialize()
    {
        // Check to see if the class has already been disposed.
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        // Initalize the ILoggerFactory and return it.
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConfiguration(this.Configuration.GetSection("Logging")).AddConsole());
        return loggerFactory;
    }

    /// <summary>
    /// Gets the <see cref="IConfiguration"/> object that provides access to configuration settings.
    /// It reads settings from the "appsettings.json" file, which is expected to be present 
    /// in the application's root directory.
    /// </summary>
    /// <remarks>
    /// The configuration is loaded at runtime and can be used to access application settings, 
    /// including logging configuration.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown if the object has already been disposed.</exception>
    public virtual IConfiguration Configuration
    {
        get
        {
            // Check to see if the class has already been disposed.
            ObjectDisposedException.ThrowIf(this.isDisposed, this);

            // Create a configuration object that only loads appsettings.json.
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Build and return the IConfiguration object.
            return builder.Build();
        }
    }
}