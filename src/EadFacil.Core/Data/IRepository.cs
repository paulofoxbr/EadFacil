using EadFacil.Core.DomainObjects;

namespace EadFacil.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }   
}