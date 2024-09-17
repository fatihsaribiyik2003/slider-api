using SubuProtokol.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.Entities.EntityFramework.Database1
{
    public class User : EntityBase<int>
    {
        public string UserName { get; set; }
        public int UserRole { get; set; }
        public string? FirstName { get; set; }
        public string? FamilyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

    }
}
