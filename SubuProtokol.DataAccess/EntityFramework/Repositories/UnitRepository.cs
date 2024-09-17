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
    public class UnitRepository :EFRepository<Unit,int,Databse1Context>,IUnitRepository
    {

        public UnitRepository(Databse1Context context) : base(context)
        {

        }
    }

    public interface IUnitRepository: IEFRepository<Unit, int>
    {

    }
}
