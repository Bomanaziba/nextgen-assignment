using Microsoft.EntityFrameworkCore;

using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Data
{
    public class CalculatorContext(DbContextOptions<CalculatorContext> options) : DbContext(options)
    {

        public DbSet<CalculatorHistory> CalculatorHistory { get; set; }
        public DbSet<PostalCode> PostalCode { get; set; }
        public DbSet<PostalCode> CalculatorSetting { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>()
                .HasData(GetPostalCodes());

            modelBuilder.Entity<CalculatorSetting>()
                .HasData(GetCalculatorSettings());
        }

        private static IEnumerable<PostalCode> GetPostalCodes()
        {
            return new List<PostalCode>()
            {
                new() { Id = 1, Calculator = CalculatorType.Progressive, Code = "7441" },
                new() { Id = 2, Calculator = CalculatorType.FlatValue, Code = "A100" },
                new() { Id = 3, Calculator = CalculatorType.FlatRate, Code = "7000" },
                new() { Id = 4, Calculator = CalculatorType.Progressive, Code = "1000" },
            };
        }

        internal static IEnumerable<CalculatorSetting> GetCalculatorSettings()
        {
            return new List<CalculatorSetting>()
            {
                new() { Id = 1, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 10, From = 0, To = 8350 },
                new() { Id = 2, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 15, From = 8351, To = 33950 },
                new() { Id = 3, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 25, From = 33951, To = 82250 },
                new() { Id = 4, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 28, From = 82251, To = 171550 },
                new() { Id = 5, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 33, From = 171551, To = 372950 },
                new() { Id = 6, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 35, From = 372951, To = null },

                new() { Id = 7, Calculator = CalculatorType.FlatValue, RateType = RateType.Percentage, Rate = 5, From = 0, To = 199999 },
                new() { Id = 8, Calculator = CalculatorType.FlatValue, RateType = RateType.Amount, Rate = 10000, From = 200000, To = null },

                new() { Id = 9, Calculator = CalculatorType.FlatRate, RateType = RateType.Percentage, Rate = 17.5M, From = 0, To = null },
            };
        }
    }
}