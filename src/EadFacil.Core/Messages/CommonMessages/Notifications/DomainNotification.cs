using MediatR;
namespace EadFacil.Core.Messages.CommonMessages.Notifications;

public class DomainNotification : Message, INotification
{
    public DateTime Timestamp { get; private set; }
    public Guid DomainNotificationId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public int Version { get; private set; }
    public DomainNotification(string key, string value, int version = 1)
    {
        Key = key;
        Value = value;
        Version = version;
        Timestamp = DateTime.Now;
        DomainNotificationId = Guid.NewGuid();
    }
}