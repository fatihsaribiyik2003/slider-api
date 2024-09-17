using Microsoft.EntityFrameworkCore;
using SubuProtokol.Entities.EntityFramework.Database1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubuProtokol.DataAccess.EntityFramework.Context
{
   
    public class Databse1Context : DbContext
    {
        public Databse1Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Protokol> Protokol { get; set; }
        public DbSet<Unit> Unit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        
        }
    }


}
