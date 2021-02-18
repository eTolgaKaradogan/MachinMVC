using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_DataEntities.Entities
{
    public class Section : ObligatoryProperities
    {
        public string Name { get; set; }
        public int FactoryId { get; set; }

        public virtual Factory Factory { get; set; }

        public  List<Machine> Machines { get; set; }
    }
}
