using Microsoft.EntityFrameworkCore;

namespace PrintQuotesCalculator.Utilities
{
    internal interface IQuoteContext
    {
        DbSet<Material> Materials { get; set; }
        DbSet<Ink> Inks { get; set; }
        DbSet<Quote> Rows { get; set; }
    }
}
