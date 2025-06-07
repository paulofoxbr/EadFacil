using EadFacil.Core.Communication.Mediator;
using MediatR;

namespace EadFacil.BC.Conteudo.Domain.Event;

public class CursoEventHandler : INotificationHandler<CursoValidacoEvent>
{
    private readonly IMediatorHandler _mediatorHandler;
    public async Task Handle(CursoValidacoEvent notification,IMediatorHandler mediatorHandler  )
    {
        //_mediatorHandler = mediatorHandler;
    }

    public async Task Handle(CursoValidacoEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}