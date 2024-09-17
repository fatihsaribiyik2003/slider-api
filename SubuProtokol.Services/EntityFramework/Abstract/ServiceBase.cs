using AutoMapper;
using SubuProtokol.DataAccess.Base.EntityFramework;
using SubuProtokol.DataAccess.EntityFramework.Repositories;
using SubuProtokol.Entities.Base;
using SubuProtokol.Entities.EntityFramework.Database1;
using SubuProtokol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Services.EntityFramework.Abstract
{
    public interface IService
    {
        int Save();
    }


    public interface IServiceBase<TEntity, TKey> : IService
    {
        TEntity Create(TEntity model);
        TEntity Create<T>(T model);
        void Delete(TKey id);
        TEntity Find(TKey id);
        T Find<T>(TKey id);
        IEnumerable<TEntity> List();
        IEnumerable<T> List<T>();
        IQueryable<TEntity> Query();
        void Update(TKey id, TEntity model);
        void Update<T>(TKey id, T model);
    }

    public class ServiceBase<TEntity, TKey, TRepository> : IServiceBase<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TRepository : IEFRepository<TEntity, TKey>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;

        public ServiceBase(TRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Repository instance can not be null.");
            _mapper = mapper;
        }

        public virtual TEntity Create(TEntity model)
        {
            return _repository.Add(model);
        }

        public virtual TEntity Create<T>(T model)
        {
            if (_mapper == null)
                throw new ArgumentNullException(nameof(_mapper), "AutoMapper parameter can not be null.");

            return _repository.Add(_mapper.Map<TEntity>(model));
        }

        public virtual void Delete(TKey id)
        {
            _repository.Remove(id);
        }

        public virtual TEntity Find(TKey id)
        {
            return _repository.Get(id);
        }

        public virtual T Find<T>(TKey id)
        {
            if (_mapper == null)
                throw new ArgumentNullException(nameof(_mapper), "AutoMapper parameter can not be null.");

            return _mapper.Map<T>(_repository.Get(id));
        }

        public virtual IEnumerable<TEntity> List()
        {
            return _repository.GetAll();
        }

        public virtual IEnumerable<T> List<T>()
        {
            if (_mapper == null)
                throw new ArgumentNullException(nameof(_mapper), "AutoMapper parameter can not be null.");

            return _mapper.Map<List<T>>(_repository.GetAll());
            //return _repository.GetAll().Select(x => _mapper.Map<T>(x)).ToList();
        }

        public virtual void Update(TKey id, TEntity model)
        {
            _repository.Update(id, model);
        }

        public virtual void Update<T>(TKey id, T model)
        {
            if (_mapper == null)
                throw new ArgumentNullException(nameof(_mapper), "AutoMapper parameter can not be null.");

            TEntity entity = Find(id);
            _mapper.Map(model, entity);

            _repository.Update(id, entity);
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _repository.Queryable();
        }

        public virtual int Save()
        {
            return _repository.Save();
        }
    }
}
