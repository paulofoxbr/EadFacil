using EadFacil.Core.Messages.CommonMessages.DomainEvents;

namespace EadFacil.BC.Conteudo.Domain.Event;

public class CursoValidacoEvent : DomainEvent
{
    public string ErrorMessage { get; private set; }
    public CursoValidacoEvent(Guid aggregateId,string error) : base(aggregateId)
    {
        ErrorMessage = error;
    }
}