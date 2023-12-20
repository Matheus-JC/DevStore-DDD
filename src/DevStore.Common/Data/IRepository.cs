using DevStore.Common.DomainObjects;

namespace DevStore.Common.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
