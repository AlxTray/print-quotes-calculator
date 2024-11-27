namespace PrintQuotesCalculator.Models
{
    internal class QuoteCalculator : IQuoteCalculator
    {
        public decimal CalculateQuote(decimal materialUsage, decimal materialCost, decimal inkUsage, decimal inkCost)
        {
            return (materialCost * materialUsage) + (inkCost * inkUsage);
        }
    }
}
