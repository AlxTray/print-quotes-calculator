using print_quotes_calculator.Utilities;

namespace print_quotes_calculator.Model
{
    internal class QuoteCalculator : IQuoteCalculator
    {
        public decimal CalculateQuote(decimal materialUsage, decimal materialCost, decimal inkUsage, decimal inkCost)
        {
            return (materialCost * materialUsage) + (inkCost * inkUsage);
        }
    }
}
