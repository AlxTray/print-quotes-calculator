using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace PrintQuotesCalculator.Utilities
{
    internal class QuoteContext : DbContext, IQuoteContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Ink> Inks { get; set; }
        public DbSet<Quote> Rows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Place the database file in current user's AppData/Roaming folder
            // TODO: Maybe should have the database file just next to the EXE when running as debug
            // TODO: Should have ability to install as all users and have it place the file in ProgramData
            var databaseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Print Quotes Calculator");
            if (!Directory.Exists(databaseFilePath))
            {
                Directory.CreateDirectory(databaseFilePath);
            }
            optionsBuilder.UseSqlite(
                $"Data Source={Path.Combine(databaseFilePath, "local_storage.db")}");
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
