namespace print_quotes_calculator.Utilities
{
    internal interface IQuoteRow
    {
        string Material { get; set; }
        decimal MaterialUsage { get; set; }
        string Ink { get; set; }
        decimal InkUsage { get; set; }
        string Description { get; set; }
        decimal QuoteCost { get; set; }
    }
}
