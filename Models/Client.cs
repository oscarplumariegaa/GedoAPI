using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        public string NameClient { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string CIF { get; set; }

        public int PhoneNumber { get; set; }
    }
}
