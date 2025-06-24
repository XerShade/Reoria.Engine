using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Common.Security.Cryptography.Factories;
using Reoria.Engine.Container.Registrars;
using Reoria.Engine.Container.Services.Interfaces;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;

namespace Reoria.Engine.Common.Security.Cryptography.Registrars;

public class HashGeneratorRegistrar : IServiceRegistrar, IServiceConfigurationRegistrar
{
    public void ConfigureServices(IServiceProvider provider)
        => HashGeneratorFactory.SetServiceProvider(provider);

    public void RegisterServices(IServiceRegistryGuard registryGuard)
        => registryGuard.TryRegister<IHashGenerator, HashGenerator>(ServiceLifetime.Transient);
}
