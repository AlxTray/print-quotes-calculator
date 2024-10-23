using Microsoft.EntityFrameworkCore;
using print_quotes_calculator.Utilities;
using Unity;

namespace print_quotes_calculator.Models
{
    internal class DatabaseHelper : IDatabaseHelper
    {
        private readonly UnityContainer _container;
        private readonly QuoteContext _quoteContext;

        public DatabaseHelper(UnityContainer container)
        {
            _container = container;
            _quoteContext = _container.Resolve<QuoteContext>();
            _quoteContext.Database.Migrate();
        }

        public IQueryable GetQuoteRows()
        {
            return _quoteContext.Rows.Select(quote => new QuoteRow(quote.QuoteId)
            {
                Material = quote.Material,
                MaterialUsage = quote.MaterialUsage,
                Ink = quote.Ink,
                InkUsage = quote.InkUsage,
                Description = quote.Description,
                QuoteCost = quote.QuoteCost
            });
        }

        public void AddOrUpdateQuoteRow(QuoteRow quoteRow)
        {
            var quote = new Quote()
            {
                QuoteId = quoteRow.Id,
                Material = quoteRow.Material,
                MaterialUsage = quoteRow.MaterialUsage,
                Ink = quoteRow.Ink,
                InkUsage = quoteRow.InkUsage,
                Description = quoteRow.Description,
                QuoteCost = quoteRow.QuoteCost
            };
            var existingQuote = _quoteContext.Rows.Find(quote.QuoteId);
            if (existingQuote == null) _quoteContext.Rows.Add(quote);
            else _quoteContext.Entry(existingQuote).CurrentValues.SetValues(quote);
            _quoteContext.SaveChanges();
        }

        public void RemoveQuoteRow(QuoteRow quoteRow)
        {
            var quote = new Quote()
            {
                QuoteId = quoteRow.Id,
                Material = quoteRow.Material,
                MaterialUsage = quoteRow.MaterialUsage,
                Ink = quoteRow.Ink,
                InkUsage = quoteRow.InkUsage,
                Description = quoteRow.Description,
                QuoteCost = quoteRow.QuoteCost
            };
            var existingQuote = _quoteContext.Rows.Find(quote.QuoteId);
            if (existingQuote != null) _quoteContext.Rows.Remove(existingQuote);
            _quoteContext.SaveChanges();
        }


        public Dictionary<string, decimal> GetMaterials()
        {
            return _quoteContext.Materials.ToDictionary(material => material.Name, material => material.Cost);
        }

        public void AddMaterial(string name, decimal cost)
        {
            _quoteContext.Materials.Add(new Material()
            {
                Name = name,
                Cost = cost
            });
            _quoteContext.SaveChanges();
        }

        public void RemoveMaterial(string name)
        {
            var existingMaterial = _quoteContext.Materials.Find(name);
            if (existingMaterial != null) _quoteContext.Materials.Remove(existingMaterial);
            _quoteContext.SaveChanges();
        }

        public Dictionary<string, decimal> GetInks()
        {
            return _quoteContext.Inks.ToDictionary(ink => ink.Name, ink => ink.Cost);
        }

        public void AddInk(string name, decimal cost)
        {
            _quoteContext.Inks.Add(new Ink()
            {
                Name = name,
                Cost = cost
            });
            _quoteContext.SaveChanges();
        }

        public void RemoveInk(string name)
        {
            var existingInk = _quoteContext.Inks.Find(name);
            if (existingInk != null) _quoteContext.Inks.Remove(existingInk);
            _quoteContext.SaveChanges();
        }
    }
}
