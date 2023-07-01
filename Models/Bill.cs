using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Bill
    {
        [Key]
        public int IdBill { get; set; }

        public int IdUser { get; set; }

        public int IdClient { get; set; }

        public int IdBudget { get; set; }

        public string? NameBill { get; set; }

    }
}
