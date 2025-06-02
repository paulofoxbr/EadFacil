using System.Data;
using EadFacil.Core.Messages;

namespace EadFacil.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; }
    private List<Event>  _events;
    public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
    
    public void AddEvent(Event eventItem)
    {
        _events ??= new List<Event>();
        _events.Add(eventItem);
    }
    public void RemoveEvent(Event eventItem)
    {
        _events?.Remove(eventItem);
    }
    public void ClearEvents()
    {
        _events?.Clear();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Entity entity) return false;
        
        if (ReferenceEquals(this, entity)) return true;
        if (GetType() != entity.GetType()) return false;
        
        return Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (ReferenceEquals(a,null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
        return a.Equals(b);
    }
    
    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }
    
    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}