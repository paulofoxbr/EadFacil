﻿using MediatR;
namespace EadFacil.Core.Messages.CommonMessages.DomainEvents;

public abstract class DomainEvent : Event, INotification
{
    public DateTime TimeStamp { get; private set; }

    protected DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
        TimeStamp = DateTime.Now;
    }
}