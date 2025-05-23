﻿using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using CsvHelper;
using PrintQuotesCalculator.Utilities;

namespace PrintQuotesCalculator.Models
{
    internal class CsvWrapper : ICsvWrapper
    {
        private readonly IDatabaseHelper _databaseHelper;

        public CsvWrapper(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

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

        public Dictionary<string, decimal> ReadMaterials(string csvPath, IDictionary<string, decimal> materials)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Material>();

            foreach (var record in records)
            {
                materials.Add(record.Name, record.Cost);
                _databaseHelper.AddMaterial(record.Name, record.Cost);
            }
            // Idk: property will not be set otherwise when return value is set to SelectedCollection
            return materials.ToDictionary();
        }

        public void WriteMaterials(string csvPath, IDictionary<string, decimal> materials)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<Material>();
            csv.NextRecord();
            csv.WriteRecords(materials);
        }

        public Dictionary<string, decimal> ReadInks(string csvPath, IDictionary<string, decimal> inks)
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Ink>();

            foreach (var record in records)
            {
                inks.Add(record.Name, record.Cost);
                _databaseHelper.AddInk(record.Name, record.Cost);
            }
            // Idk: property will not be set otherwise when return value is set to SelectedCollection
            return inks.ToDictionary();
        }

        public void WriteInks(string csvPath, IDictionary<string, decimal> inks)
        {
            using var writer = new StreamWriter(csvPath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteHeader<Ink>();
            csv.NextRecord();
            csv.WriteRecords(inks);
        }
    }
}
