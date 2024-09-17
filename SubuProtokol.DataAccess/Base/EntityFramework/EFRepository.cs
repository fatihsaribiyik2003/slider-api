using Microsoft.EntityFrameworkCore;
using SubuProtokol.Core.Enums;
using SubuProtokol.Entities.Base;
using SubuProtokol.Models;
using System.Linq.Expressions;

namespace SubuProtokol.DataAccess.Base.EntityFramework
{

    public abstract class EFRepository<TEntity, TKey, TContext> : IEFRepository<TEntity, TKey>
            where TContext : DbContext
            where TEntity : EntityBase<TKey>
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _table;
        //private readonly IMapper _mapper;

        public EFRepository(TContext context/*, IMapper mapper*/)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
            _table = _context.Set<TEntity>();
            //_mapper = mapper;
        }

        public virtual TEntity Get(TKey id)
        {
            return _table.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _table.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _table.Where(predicate).ToList();
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return _table.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">For all table count give null.</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null ? _table.Count(predicate) : _table.Count();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _table.Add(entity).Entity;
        }

        public virtual void Update(TKey id, TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;

            //TEntity dbEntity = Get(id);
            //_mapper.Map(entity, dbEntity);
        }

        public virtual void Remove(TKey id)
        {
            _table.Remove(Get(id));
        }

        public virtual void AttachIfNot(TEntity entity)
        {
            if (!_table.Local.Contains(entity))
            {
                _table.Attach(entity);
            }
        }

        public virtual int Save()
        {

            return _context.SaveChanges();
        }
    }
}
