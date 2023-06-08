using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
