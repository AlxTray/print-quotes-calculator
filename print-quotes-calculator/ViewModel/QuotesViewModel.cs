using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Unity;

namespace print_quotes_calculator.ViewModel
{
    internal class QuotesViewModel : IQuotesViewModel, INotifyPropertyChanged
    {
        private readonly UnityContainer _container;
        private ObservableCollection<QuoteRow> _quoteRows;
        private ObservableCollection<string> _materialTypes;
        private ObservableCollection<string> _inkTypes;

        public QuotesViewModel(UnityContainer container)
        {
            _container = container;
            _quoteRows = [];
            AddCommand = new RelayCommand(AddQuoteRow);

            _materialTypes = [
                "A",
                "B",
                "C"
            ];

            _inkTypes = [
                "D",
                "E",
                "F"
            ];
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

        public ObservableCollection<string> MaterialTypes
        {
            get => _materialTypes;
            set
            {
                _materialTypes = value;
                RaisePropertyChanged(nameof(MaterialTypes));
            }
        }

        public ObservableCollection<string> InkTypes
        {
            get => _inkTypes;
            set
            {
                _inkTypes = value;
                RaisePropertyChanged(nameof(MaterialTypes));
            }
        }

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
