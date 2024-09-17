using SubuProtokol.Entities.Base;
using System.Linq.Expressions;

namespace SubuProtokol.DataAccess.Base.EntityFramework
{
    public interface IEFRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        TEntity Add(TEntity entity);
        int Count(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Queryable();
        void Remove(TKey id);
        int Save();
        void Update(TKey id, TEntity entity);
    }
}
