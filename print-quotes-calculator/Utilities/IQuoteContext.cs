using Microsoft.EntityFrameworkCore;

namespace print_quotes_calculator.Utilities
{
    internal interface IQuoteContext
    {
        DbSet<Material> Materials { get; set; }
        DbSet<Ink> Inks { get; set; }
        DbSet<Quote> Rows { get; set; }
    }
}
