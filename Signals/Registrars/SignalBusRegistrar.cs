using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container.Registrars;
using Reoria.Engine.Signals.Interfaces;

namespace Reoria.Engine.Signals.Registrars;

public class SignalBusRegistrar : IServiceRegistrar
{
    public void RegisterServices(IServiceCollection services) 
        => services.AddSingleton<ISignalBus, SignalBus>();
}
