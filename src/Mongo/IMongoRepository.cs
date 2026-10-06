using Griffin.Core.Model;

namespace Griffin.Mongo;

public interface IMongoRepository<TEntity, in TId> : IRepository<TEntity, TId>
    where TEntity : class, IAggregate<TId>
{
}