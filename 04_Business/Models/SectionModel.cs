using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Business.Models
{
    public class SectionModel : ObligatoryProperities
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int FactoryId { get; set; }
    }
}
