using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_DataEntities.Entities
{
    public class User : ObligatoryProperities
    {
      
        public string Name { get; set; }
      
        public string Password { get; set; }
        
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int FactoryId { get; set; }

        public virtual Factory Factory { get; set; }

        public string Mail { get; set; }
    }
}
