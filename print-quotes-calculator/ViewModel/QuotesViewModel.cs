using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
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
            AddCommand = new RelayCommand(AddQuoteRow);

            var databaseHelper = _container.Resolve<DatabaseHelper>();
            _materials = databaseHelper.GetMaterials();
            _inks = databaseHelper.GetInks();

            // Testing
            _materials = new Dictionary<string, decimal>
            {
                { "Material 1", 1.0m },
                { "Material 2", 2.0m },
                { "Material 3", 3.0m }
            };
            _inks = new Dictionary<string, decimal>
            {
                { "Ink 1", 1.0m },
                { "Ink 2", 2.0m },
                { "Ink 3", 3.0m }
            };
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
