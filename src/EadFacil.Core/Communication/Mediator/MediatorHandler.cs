using EadFacil.Core.Data.EventSourcing;
using EadFacil.Core.Messages;
using EadFacil.Core.Messages.CommonMessages.DomainEvents;
using EadFacil.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace EadFacil.Core.Communication.Mediator;

public class MediatorHandler
{
    private readonly IMediator _mediator;
    private readonly IEventSourcingRepository _eventSourcingRepository;
   
    public MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
    {
        _mediator = mediator;
        _eventSourcingRepository = eventSourcingRepository;
    }
    
    public async Task PublishEventAsync<T>(T message) where T : Event
    {
        await _mediator.Publish(message);
        await _eventSourcingRepository.SaveEvent(message);
    }
    
    public async Task<bool> SendCommandAsync<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    public async Task PublishNotificationAsync<T>(T notification) where T : DomainNotification
    {
        await _mediator.Publish(notification);
    }
    
    public async Task PublishDomainEventAsync<T>(T domainEvent) where T : DomainEvent
    {
        await _mediator.Publish(domainEvent);
    }
    
}