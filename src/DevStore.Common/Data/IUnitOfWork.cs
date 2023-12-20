namespace DevStore.Common.Data;

public interface IUnitOfWork 
{
    Task<bool> Commit();
}
