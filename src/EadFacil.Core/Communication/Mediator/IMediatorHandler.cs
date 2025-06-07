using EadFacil.Core.Messages;
using EadFacil.Core.Messages.CommonMessages.DomainEvents;
using EadFacil.Core.Messages.CommonMessages.Notifications;

namespace EadFacil.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(T message) where T : Event;
    Task<bool> EnviarComando<T>(T comando) where T : Command;
    
    Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
}