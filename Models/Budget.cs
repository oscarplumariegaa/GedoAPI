using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Budget
    {
        [Key]
        public int IdBudget { get; set; }

        public int IdUser { get; set; }

        public int IdClient { get; set; }

        public int? IdBill { get; set; }

        public decimal Import { get; set; }

        public decimal ImportIVA { get; set; }

        public string NameBudget { get; set; }

        public string? NameClient { get; set; }

    }
}
