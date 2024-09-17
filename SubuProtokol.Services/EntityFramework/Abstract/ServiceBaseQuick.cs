using AutoMapper;
using SubuProtokol.DataAccess.Base.EntityFramework;
using SubuProtokol.Entities.Base;

namespace SubuProtokol.Services.EntityFramework.Abstract
{
    public class ServiceBaseQuick<TEntity, TKey, TRepository> : ServiceBase<TEntity, TKey, TRepository>
        where TEntity : EntityBase<TKey>
        where TRepository : IEFRepository<TEntity, TKey>
    {
        public ServiceBaseQuick(TRepository repository, IMapper mapper) : 
            base(repository, mapper)
        {
        }

        public override TEntity Create(TEntity model)
        {
            TEntity entity = base.Create(model);
            Save();

            return entity;
        }

        public override TEntity Create<T>(T model)
        {
            TEntity entity = base.Create<T>(model);
            Save();

            return entity;
        }

        public override void Update(TKey id, TEntity model)
        {
            base.Update(id, model);
            Save();
        }

        public override void Update<T>(TKey id, T model)
        {
            base.Update(id, model);
            Save();
        }

        public override void Delete(TKey id)
        {
            base.Delete(id);
            Save();
        }
    }
}
