using MediatR;

namespace EadFacil.Core.Messages.CommonMessages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _domainNotifications;
    private bool _disposed ;
    
    public DomainNotificationHandler()
    {
        _domainNotifications = new List<DomainNotification>();
    }

    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification), "Notification cannot be null");
        }
        _domainNotifications.Add(notification);
        return Task.CompletedTask;
    }
    public virtual List<DomainNotification> GetNotifications()
    {
        ThrowIfDisposed();
        return _domainNotifications;
    }
    public virtual bool HasNotifications()
    {
        ThrowIfDisposed();
        return _domainNotifications.Any();
    }
    public virtual void ClearNotifications()
    {
        ThrowIfDisposed();
        _domainNotifications.Clear();
    }
    
    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(DomainNotificationHandler));
        }
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            _domainNotifications?.Clear();
            _disposed = true;
        }
    }

}