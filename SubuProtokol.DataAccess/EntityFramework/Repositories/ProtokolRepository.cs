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
    public class ProtokolRepository : EFRepository<Protokol, int, Databse1Context>, IProtokolRepository
    {
        public ProtokolRepository(Databse1Context context) : base(context)
        {
        }
    }

   public  interface IProtokolRepository : IEFRepository<Protokol, int>
    {
    }
}
