using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        public string Name { get; set; }

        public string Email { get; set; } 

        public string Password { get; set; }
        
        public string Address { get; set; }

        public string CIF { get; set; }

        public int PhoneNumber { get; set; }

    }
}
