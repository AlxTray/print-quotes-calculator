using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace print_quotes_calculator.ViewModel
{
    internal interface IQuotesViewModel
    {
        ObservableCollection<QuoteRow> QuoteRows { get; set; }
        Dictionary<string, decimal> MaterialTypes { get; set; }
        Dictionary<string, decimal> InkTypes { get; set; }
        ICommand AddCommand { get; }
    }
}
