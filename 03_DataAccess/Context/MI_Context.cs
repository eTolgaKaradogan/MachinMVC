using _02_DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_DataAccess.Context
{
    public class MI_Context : DbContext
    {
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MI_Context() : base(MI_Config.Config.connectionString)
        {

        }

       
    }
}
