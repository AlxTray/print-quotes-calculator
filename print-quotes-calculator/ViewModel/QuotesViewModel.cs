using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using print_quotes_calculator.Model;
using print_quotes_calculator.Utilities;
using Unity;
using Unity.Resolution;

namespace print_quotes_calculator.ViewModel
{
    internal class QuotesViewModel : IQuotesViewModel, INotifyPropertyChanged
    {
        private readonly UnityContainer _container;
        private ObservableCollection<QuoteRow> _quoteRows;
        private Dictionary<string, decimal> _materials;
        private Dictionary<string, decimal> _inks;

        public QuotesViewModel(UnityContainer container)
        {
            _container = container;
            AddCommand = new RelayCommand(AddQuoteRow);

            var db = _container.Resolve<QuoteContext>();
            db.Database.Migrate();
            _materials = db.Materials.ToDictionary(material => material.Name, material => material.Cost);
            _inks = db.Inks.ToDictionary(ink => ink.Name, ink => ink.Cost);

            _quoteRows = new ObservableCollection<QuoteRow>(db.Rows.Select(quote => new QuoteRow(quote.QuoteId)
            {
                Material = quote.Material,
                MaterialUsage = quote.MaterialUsage,
                Ink = quote.Ink,
                InkUsage = quote.InkUsage,
                Description = quote.Description,
                QuoteCost = quote.QuoteCost
            }));
            _quoteRows.CollectionChanged += QuoteRow_CollectionChanged;
        }

        public ObservableCollection<QuoteRow> QuoteRows
        {
            get => _quoteRows;
            set
            {
                _quoteRows = value;
                RaisePropertyChanged(nameof(QuoteRows));
            }
        }

        public Dictionary<string, decimal> MaterialTypes
        {
            get => _materials;
            set
            {
                _materials = value;
                RaisePropertyChanged(nameof(MaterialTypes));
            }
        }

        public IEnumerable<string> MaterialTypesKeys => MaterialTypes.Keys;

        public Dictionary<string, decimal> InkTypes
        {
            get => _inks;
            set
            {
                _inks = value;
                RaisePropertyChanged(nameof(MaterialTypes));
            }
        }

        public IEnumerable<string> InkTypesKeys => InkTypes.Keys;


        public ICommand AddCommand { get; }

        public void AddQuoteRow()
        {
            long quoteId = 1;
            if (_quoteRows.Count > 0)
            {
                quoteId = _quoteRows.Last().Id + 1;
            }
            _quoteRows.Add(_container.Resolve<QuoteRow>(new ParameterOverride("id", quoteId)));
        }


        private void QuoteRow_CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.NewItems != null)
            {
                foreach (QuoteRow quoteRow in eventArgs.NewItems)
                {
                    quoteRow.PropertyChanged += QuoteRow_PropertyChanged;
                }
            }

            if (eventArgs.OldItems != null)
            {
                foreach (QuoteRow quoteRow in eventArgs.OldItems)
                {
                    quoteRow.PropertyChanged -= QuoteRow_PropertyChanged;
                }
            }
        }

        public void QuoteRow_PropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (sender is not QuoteRow quoteRow) return;
            // Will have infinite recursion if QuoteCost is set here, which causes a PropertyChanged event
            if (eventArgs.PropertyName is "QuoteCost") return;

            decimal materialCost;
            decimal inkCost;
            try
            {
                materialCost = MaterialTypes[quoteRow.Material];
                inkCost = InkTypes[quoteRow.Ink];
            }
            catch (KeyNotFoundException e)
            {
                quoteRow.QuoteCost = 0.00m;
                return;
            }
            quoteRow.QuoteCost = _container.Resolve<QuoteCalculator>()
                .CalculateQuote(quoteRow.MaterialUsage, materialCost, quoteRow.InkUsage, inkCost);

            var db = _container.Resolve<QuoteContext>();
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
            System.Diagnostics.Debug.WriteLine($"QuoteRow_PropertyChanged: {quote.QuoteId}");
            var existingQuote = db.Rows.Find(quote.QuoteId);
            if (existingQuote == null) db.Rows.Add(quote);
            else db.Entry(existingQuote).CurrentValues.SetValues(quote);
            db.SaveChanges();
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
