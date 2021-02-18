using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Business.Models
{
    public class HatırlatıcıModel : ObligatoryProperities
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Details { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string GmailPassword { get; set; }
    }
}
