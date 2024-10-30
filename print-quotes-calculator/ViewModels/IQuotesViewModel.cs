using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using print_quotes_calculator.Utilities;

namespace print_quotes_calculator.ViewModels
{
    internal interface IQuotesViewModel
    {
        ObservableCollection<QuoteRow> QuoteRows { get; set; }
        Dictionary<string, decimal> MaterialTypes { get; set; }
        Dictionary<string, decimal> InkTypes { get; set; }
        ICommand AddCommand { get; }
        void AddQuoteRow();
        ICommand SaveCommand { get; }
        void SaveQuoteRows();
        ICommand OpenCommand { get; }
        void OpenQuoteRows();
        void QuoteRow_CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs);
        void QuoteRow_PropertyChanged(object sender, PropertyChangedEventArgs eventArgs);
    }
}
