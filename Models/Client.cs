using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        public string NameClient { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CIF { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }
    }
}
