namespace print_quotes_calculator.Models
{
    internal interface IQuoteCalculator
    {
        decimal CalculateQuote(decimal materialUsage, decimal materialCost, decimal inkUsage, decimal inkCost);
    }
}
