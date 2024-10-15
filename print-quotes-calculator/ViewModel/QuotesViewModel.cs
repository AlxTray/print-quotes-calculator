using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace print_quotes_calculator.ViewModel
{
    internal class QuotesViewModel : IQuotesViewModel
    {
        private UnityContainer _container;
        private ObservableCollection<QuoteRow> _quoteRows;

        public QuotesViewModel(UnityContainer container)
        {
            _container = container;
            _quoteRows = [];
            AddCommand = new RelayCommand(AddNewQuoteRow);
        }

        public ICommand AddCommand { get; }

        public void addQuoteRow()
        {
            _quoteRows.Add(_container.Resolve<QuoteRow>());
        }
    }
}
