using PrintQuotesCalculator.Utilities;

namespace PrintQuotesCalculator.Models
{
    internal interface IDatabaseHelper
    {
        IQueryable GetQuoteRows();
        void AddOrUpdateQuoteRow(QuoteRow quoteRow);
        void RemoveQuoteRow(QuoteRow quoteRow);
        Dictionary<string, decimal> GetMaterials();
        void AddMaterial(string name, decimal cost);
        void RemoveMaterial(string name);
        Dictionary<string, decimal> GetInks();
        void AddInk(string name, decimal cost);
        void RemoveInk(string name);
    }
}
