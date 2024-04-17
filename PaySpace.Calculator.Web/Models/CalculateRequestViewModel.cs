using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Web.Models
{
    public sealed class CalculateRequestViewModel
    {
        [Required]
        public string? PostalCode { get; set; }

        [Required]
        [Range(0.001, double.MaxValue, ErrorMessage = "Income must be greater than 0")]
        public decimal Income { get; set; }
    }
}