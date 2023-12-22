namespace DevStore.Common.Data;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
}
