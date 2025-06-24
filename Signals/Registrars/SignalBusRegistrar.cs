using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container.Registrars;
using Reoria.Engine.Container.Services.Interfaces;
using Reoria.Engine.Signals.Interfaces;

namespace Reoria.Engine.Signals.Registrars;

public class SignalBusRegistrar : IServiceRegistrar
{        
    public void RegisterServices(IServiceRegistryGuard registryGuard)
        => registryGuard.TryRegister<ISignalBus, SignalBus>(ServiceLifetime.Singleton);
}
