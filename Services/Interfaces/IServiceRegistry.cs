namespace Reoria.Engine.Services.Interfaces;

public interface IServiceRegistry
{
    TService FetchService<TService>();
    void RegisterService<TService>(TService instance);
    void RemoveService<TService>();
    void UpdateService<TService>(TService instance);
}
