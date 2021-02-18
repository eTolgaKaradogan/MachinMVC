using AppCore.DataAccess.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Business.Models
{
    public class UserModel : ObligatoryProperities
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string PasswordAgain { get; set; }
        [Required]
        public int FactoryId { get; set; }

        [Required]
        public  string Mail { get; set; }

        [Required]
        public string MailAgain { get; set; }
    }
}
