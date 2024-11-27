using PrintQuotesCalculator.Utilities;

namespace PrintQuotesCalculator.Models
{
    internal interface IDatabaseHelper
    {
        IQueryable GetQuoteRows();
        void AddOrUpdateQuoteRow(QuoteRow quoteRow);
        void RemoveQuoteRow(QuoteRow quoteRow);
        Dictionary<string, decimal> GetMaterials();
        void AddMaterial(string material, decimal cost);
        void RemoveMaterial(string material);
        Dictionary<string, decimal> GetInks();
        void AddInk(string ink, decimal cost);
        void RemoveInk(string ink);
    }
}
