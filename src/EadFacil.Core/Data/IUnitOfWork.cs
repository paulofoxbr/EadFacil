namespace EadFacil.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();   
}