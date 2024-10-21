using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using print_quotes_calculator.Model;
using print_quotes_calculator.Utilities;
using Unity;

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

            _quoteRows = [];
            _quoteRows.CollectionChanged += QuoteRow_CollectionChanged;

            AddCommand = new RelayCommand(AddQuoteRow);

            var db = _container.Resolve<QuoteContext>();
            db.Database.Migrate();
            _materials = db.Materials.ToDictionary(material => material.Name, material => material.Cost);
            _inks = db.Inks.ToDictionary(ink => ink.Name, ink => ink.Cost);
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
            _quoteRows.Add(_container.Resolve<QuoteRow>());
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
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
