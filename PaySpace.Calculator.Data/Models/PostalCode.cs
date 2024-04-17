using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models
{
    public sealed class PostalCode
    {
        [Key]
        public long Id { get; set; }

        public string Code { get; set; }

        public CalculatorType Calculator { get; set; }
    }
}