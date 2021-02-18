using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_DataEntities.Entities
{
    public class Machine : ObligatoryProperities
    {
        public string Name { get; set; }

        public int SeriNO { get; set; }

        public string Model { get; set; }

        public string Detail { get; set; }

        public  int SectionId { get; set; }

        public Section Section { get; set; }

        public List<Hatırlatıcı> Hatırlatıcıs { get; set; }
    }
}
