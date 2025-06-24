using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Common.Security.Cryptography.Factories;
using Reoria.Engine.Container.Registrars;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;

namespace Reoria.Engine.Common.Security.Cryptography.Registrars;

public class SaltGeneratorRegistrar : IServiceRegistrar, IServiceConfigurationRegistrar
{
    public void ConfigureServices(IServiceProvider provider)
        => SaltGeneratorFactory.SetServiceProvider(provider);

    public void RegisterServices(IServiceCollection services)
        => services.AddTransient<ISaltGenerator, SaltGenerator>();
}
