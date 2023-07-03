using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Concept
    {
        [Key]
        public int IdConcept { get; set; }

        public int IdBill { get; set; }

        public int IdBudget { get; set; }

        public string? NameField { get; set; }

        public decimal Value { get; set; }
    }
}
