using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Registrars;
using Reoria.Engine.Signals.Interfaces;

namespace Reoria.Engine.Signals.Registrars;

public class SignalBusRegistrar : IServiceRegistrar
{
    public void RegisterServices(ContainerBuilder builder, IConfiguration configuration, ILoggerFactory loggerFactory)
        => builder.RegisterType<SignalBus>().As<ISignalBus>().SingleInstance();
}
