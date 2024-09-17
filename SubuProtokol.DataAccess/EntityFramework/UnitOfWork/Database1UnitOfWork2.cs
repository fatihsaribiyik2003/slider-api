using SubuProtokol.DataAccess.Base.UnitOfWork;
using SubuProtokol.DataAccess.EntityFramework.Context;

namespace SubuProtokol.DataAccess.EntityFramework.UnitOfWork
{
    public interface IDatabase1UnitOfWork2: IUnitOfWorkGeneric
    {

    }

    public class Database1UnitOfWork2 : UnitOfWorkGeneric<Databse1Context>, IDatabase1UnitOfWork2
    {
        public Database1UnitOfWork2(Databse1Context context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
