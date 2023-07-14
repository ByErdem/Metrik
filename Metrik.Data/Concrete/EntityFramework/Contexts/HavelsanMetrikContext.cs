using Metrik.Data.Concrete.EntityFramework.Mappings;
using Metrik.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Data.Concrete.EntityFramework.Contexts
{
    public class MetrikContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=Metrik;Persist Security Info=True;User ID=sa;Password=0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer("Server=89.252.187.226\\MSSQLSERVER2019;Initial Catalog=koderpar_metrik;Persist Security Info=True;User ID=koderpar_havelsanmetrik;Password=X8xq0~w96;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap()); 
        }

    }
}
