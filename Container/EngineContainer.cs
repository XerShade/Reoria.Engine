using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging.Interfaces;
using System.Reflection;

namespace Reoria.Engine.Container;

public abstract class EngineContainer<TLoggingInitalizer> : Disposable, IEngineContainer
    where TLoggingInitalizer : class, ILoggingInitializer
{
    protected ILogger<IEngineContainer> logger;
    protected IConfiguration configuration;
    protected IServiceCollection services;
    protected IServiceProvider provider;

    public ILogger<IEngineContainer> Logger => this.Logger;
    public IConfiguration Configuration => this.configuration;
    public IServiceCollection Services => this.services;
    public IServiceProvider Provider => this.provider;

    public EngineContainer()
    {
        this.logger = this.CreateEarlyLogger();
        this.logger.LogInformation("Constructing game engine container.");
        this.configuration = this.CreateEarlyConfigurationBuilder(new ConfigurationBuilder()).Build();
        this.services = new ServiceCollection();
        this.provider = this.services.BuildServiceProvider();
        this.logger.LogInformation("Constructed game engine container.");
    }

    protected virtual ILoggingInitializer CreateEarlyLoggingInitalizer<TInitalizer>()
        where TInitalizer : class, TLoggingInitalizer
        => Activator.CreateInstance<TInitalizer>() 
        ?? throw new NullReferenceException("Unable to create logging initializer.");

    protected virtual ILoggerFactory CreateEarlyLoggingFactory(ILoggingInitializer loggingInitializer)
        => loggingInitializer.Initialize() 
        ?? throw new NullReferenceException("Unable to create logger factory.");

    protected virtual ILogger<IEngineContainer> CreateEarlyLogger()
    {
        using ILoggingInitializer loggingInitializer = this.CreateEarlyLoggingInitalizer<TLoggingInitalizer>();
        using ILoggerFactory loggerFactory = this.CreateEarlyLoggingFactory(loggingInitializer);
        return loggerFactory.CreateLogger<IEngineContainer>();
    }

    protected virtual IConfigurationBuilder CreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
    {
        return builder
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly());
    }

    public virtual IEngineContainer CreateConfiguration()
    {
        lock (this.@lock)
        {
            this.logger.LogInformation("Creating engine configuration.");
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            this.OnCreateConfiguration(builder);

            this.configuration = builder.Build();
            this.logger.LogInformation("Created engine configuration.");
        }

        return this;
    }

    protected virtual void OnCreateConfiguration(IConfigurationBuilder builder)
    {
        _ = builder.AddEnvironmentVariables();
        _ = builder.AddUserSecrets(Assembly.GetExecutingAssembly());
    }

    public virtual IEngineContainer CreateServiceCollection()
    {
        lock(this.@lock)
        {
            this.logger.LogInformation("Creating engine logger and logger factory.");
            using ILoggingInitializer loggingInitializer = this.CreateLoggingInitalizer<TLoggingInitalizer>(this.configuration)
                ?? throw new NullReferenceException("Unable to create logging initializer.");
            ILoggerFactory loggerFactory = loggingInitializer.Initialize()
                ?? throw new NullReferenceException("Unable to create logger factory.");
            this.logger = loggerFactory.CreateLogger<IEngineContainer>()
                ?? throw new NullReferenceException("Unable to logger.");
            this.logger.LogInformation("Created engine logger and logger factory using '{LoggingInitalizer}'.", typeof(TLoggingInitalizer).Name);

            this.logger.LogInformation("Creating engine service collection.");
            _ = this.services.AddSingleton<ILoggerFactory>(loggerFactory);
            _ = this.services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            _ = this.services.AddSingleton<IConfiguration>(this.configuration);

            this.OnCreateServiceCollection(this.services);
            this.logger.LogInformation("Created engine service collection, {count} services have been added to the collection.", this.services.Count);
        }

        return this;
    }

    protected virtual ILoggingInitializer CreateLoggingInitalizer<TInitalizer>(IConfiguration configuration)
        where TInitalizer : class, TLoggingInitalizer
        => Activator.CreateInstance(typeof(TInitalizer), configuration) as TInitalizer
        ?? throw new NullReferenceException("Unable to create logging initializer.");

    protected virtual void OnCreateServiceCollection(IServiceCollection services) { }

    public virtual IEngineContainer CreateServiceProvider()
    {
        lock (this.@lock)
        {
            this.logger.LogInformation("Creating engine service provider.");
            this.provider = this.services.BuildServiceProvider();
            this.OnCreateServiceProvider(this.provider);
            this.logger.LogInformation("Created engine service provider, {count} services have been built and configured.", this.services.Count);
        }

        return this;
    }

    protected virtual void OnCreateServiceProvider(IServiceProvider provider) { }
}
