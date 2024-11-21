using System.Collections.ObjectModel;
using CsvHelper;
using System.Globalization;
using System.IO;
using print_quotes_calculator.Utilities;
using CsvHelper.Configuration;

namespace print_quotes_calculator.Models
{
    internal class CsvWrapper : ICsvWrapper
    {
        public ObservableCollection<QuoteRow> ReadQuotes(string csvPath, ObservableCollection<QuoteRow> quoteRows)
        {

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<QuoteRow>();

            foreach (var record in records)
            {
                quoteRows.Add(record);
            }
            return quoteRows;
        }

        public void WriteQuotes(string csvPath, ObservableCollection<QuoteRow> quoteRows)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(quoteRows);
        }

        public Dictionary<string, decimal> ReadMaterials(string csvPath)
        {
            throw new NotImplementedException();
        }

        public void WriteMaterials(string csvPath, Dictionary<string, decimal> materials)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, decimal> ReadInks(string csvPath)
        {
            throw new NotImplementedException();
        }

        public void WriteInks(string csvPath, Dictionary<string, decimal> inks)
        {
            throw new NotImplementedException();
        }
    }
}
