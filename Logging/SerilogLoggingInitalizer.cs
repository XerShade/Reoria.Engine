using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;

namespace Reoria.Engine.Logging;

/// <summary>
/// This class is responsible for initializing an <see cref="ILoggerFactory"/> 
/// to be used for logging purposes before the engine is fully initialized 
/// and its Dependency Injection (DI) container and configuration is loaded.
/// It ensures that logging is available early in the application lifecycle.
/// This implementation uses Serilog for logging instead of the default logging framework.
/// </summary>
public class SerilogLoggingInitalizer : EngineLoggingInitalizer
{
    /// <summary>
    /// Initializes an <see cref="ILoggerFactory"/> with a Serilog logger before the engine is fully initialized.
    /// This method configures Serilog for logging based on the configuration in the "Logging" section
    /// of the app configuration, and enables Serilog as the logging provider.
    /// </summary>
    /// <returns>An instance of <see cref="ILoggerFactory"/> for logging, using Serilog.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the object has already been disposed.</exception>
    public override ILoggerFactory Initialize()
    {
        // Ensure the logger is closed.
        Log.CloseAndFlush();

        // Check to see if the class has already been disposed.
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        // Create and initalize the Serilog logger.
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(this.Configuration)
            .CreateLogger();

        // Initalize the ILoggerFactory and return it.
        ILoggerFactory loggerFactory = new LoggerFactory([new SerilogLoggerProvider(Log.Logger)]);
        return loggerFactory;
    }

    /// <summary>
    /// Releases unmanaged resources used by the logger and the base class.
    /// Ensures that the Serilog logger is properly closed and flushed to release any resources.
    /// </summary>
    protected override void FreeUnmanagedObjects()
    {
        // Ensure the logger is closed.
        Log.CloseAndFlush();

        // Call the base function.
        base.FreeUnmanagedObjects();
    }
}