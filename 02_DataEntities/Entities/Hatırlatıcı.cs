using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_DataEntities.Entities
{
    public class Hatırlatıcı: ObligatoryProperities
    {
        public string Name { get; set; }

        public string Details { get; set; }

        public DateTime DateTime { get; set; }

        public int MachineId { get; set; }

        public Machine Machine { get; set; }

        public  string To{ get; set; }

        public string From { get; set; }
    }
}
