using DevStore.Common.DomainObjects;

namespace DevStore.Common.Data;

public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
}
