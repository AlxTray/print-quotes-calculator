using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace print_quotes_calculator.Utilities
{
    internal class QuoteContext : DbContext, IQuoteContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Ink> Inks { get; set; }
        public DbSet<Quote> Rows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=local_storage.db");
        }
    }

    [Table("Materials")]
    internal class Material
    {
        [Key]
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    [Table("Inks")]
    internal class Ink
    {
        [Key]
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    [Table("Quotes")]
    internal class Quote
    {
        [Key]
        public long QuoteId { get; set; }
        public string Material { get; set; }
        public decimal MaterialUsage { get; set; }
        public string Ink { get; set; }
        public decimal InkUsage { get; set; }
        public string Description { get; set; }
        public decimal QuoteCost { get; set; }
    }
}
