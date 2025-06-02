using EadFacil.Core.Messages;

namespace EadFacil.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(Event message) where T : Event;
    Task<bool> EnviarComando<T>(T comando) where T : Command;
}