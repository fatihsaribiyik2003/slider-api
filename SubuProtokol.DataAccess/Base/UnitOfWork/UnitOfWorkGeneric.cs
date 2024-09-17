using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SubuProtokol.DataAccess.Base.UnitOfWork
{
    public interface IUnitOfWorkGeneric : IUnitOfWork
    {
        TRepository GetRepository<TRepository>();
    }

    public class UnitOfWorkGeneric<TContext> : IUnitOfWorkGeneric
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkGeneric(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _serviceProvider = serviceProvider;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public TRepository GetRepository<TRepository>()
        {
            if (_repositories.ContainsKey(typeof(TRepository)) == false)
            {
                TRepository repository = _serviceProvider.GetRequiredService<TRepository>();

                _repositories.Add(typeof(TRepository), repository);
            }

            return (TRepository)_repositories[typeof(TRepository)];
        }
    }
}
