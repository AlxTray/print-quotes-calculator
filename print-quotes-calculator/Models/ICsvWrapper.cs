using System.Collections.ObjectModel;
using System.Collections.Specialized;
using print_quotes_calculator.Utilities;

namespace print_quotes_calculator.Models
{
    internal interface ICsvWrapper
    {
        ObservableCollection<QuoteRow> ReadQuotes(string csvPath, ObservableCollection<QuoteRow> quoteRows);
        void WriteQuotes(string csvPath, ObservableCollection<QuoteRow> quoteRows);
        Dictionary<string, decimal> ReadMaterials(string csvPath, Dictionary<string, decimal> materials);
        void WriteMaterials(string csvPath, Dictionary<string, decimal> materials);
        Dictionary<string, decimal> ReadInks(string csvPath, Dictionary<string, decimal> inks);
        void WriteInks(string csvPath, Dictionary<string, decimal> inks);
    }
}
