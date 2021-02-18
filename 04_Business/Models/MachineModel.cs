using _02_DataEntities.Entities;
using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Business.Models
{
    public class MachineModel : ObligatoryProperities
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int SeriNO { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        [DisplayName("Bölüm Adı")]
        public int SectionId { get; set; }
    }
}
