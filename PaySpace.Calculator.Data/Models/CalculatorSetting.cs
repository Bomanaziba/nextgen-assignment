using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models
{
    public sealed class CalculatorSetting
    {
        [Key]
        public long Id { get; set; }

        public CalculatorType Calculator { get; set; }

        public RateType RateType { get; set; }

        public decimal Rate { get; set; }

        public decimal From { get; set; }

        public decimal? To { get; set; }
    }
}