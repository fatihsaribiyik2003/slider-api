using SubuProtokol.DataAccess.Base.EntityFramework;
using SubuProtokol.DataAccess.EntityFramework.Context;
using SubuProtokol.Entities.EntityFramework.Database1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.DataAccess.EntityFramework.Repositories
{
    public interface IUserProtokolRepository : IEFRepository<User, int>
    {
    }
    public class UserProtokolRepository : EFRepository<User, int, Databse1Context>, IUserProtokolRepository
    {
        public UserProtokolRepository(Databse1Context context) : base(context)
        {
        }
    }

    
}
