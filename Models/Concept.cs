using System.ComponentModel.DataAnnotations;

namespace Gedo.Models
{
    public class Concept
    {
        [Key]
        public int IdConcept { get; set; }

        public int IdBill { get; set; }

        public int IdBudget { get; set; }

        public int NameField { get; set; }

        public int Value { get; set; }
    }
}
