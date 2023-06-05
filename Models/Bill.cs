using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Bill
    {
        [Key]
        public int IdBill { get; set; }

        public int IdUser { get; set; }

        public int IdClient { get; set; }

        public decimal Import { get; set; }

        public decimal ImportIVA { get; set; }
    }
}
