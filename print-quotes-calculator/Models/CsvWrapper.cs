using System.Collections.ObjectModel;
using CsvHelper;
using System.Globalization;
using System.IO;
using print_quotes_calculator.Utilities;

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

        public Dictionary<string, decimal> ReadMaterials(string csvPath, Dictionary<string, decimal> materials)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Material>();

            foreach (var record in records)
            {
                materials.Add(record.Name, record.Cost);
            }
            return materials;
        }

        public void WriteMaterials(string csvPath, Dictionary<string, decimal> materials)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<Material>();
            csv.NextRecord();
            csv.WriteRecords(materials);
        }

        public Dictionary<string, decimal> ReadInks(string csvPath, Dictionary<string, decimal> inks)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Ink>();

            foreach (var record in records)
            {
                inks.Add(record.Name, record.Cost);
            }
            return inks;
        }

        public void WriteInks(string csvPath, Dictionary<string, decimal> inks)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<Ink>();
            csv.NextRecord();
            csv.WriteRecords(inks);
        }
    }
}
