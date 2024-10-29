using System.Windows;
using print_quotes_calculator.ViewModels;

namespace print_quotes_calculator.Windows
{
    public partial class QuoteWindow : Window
    {
        internal QuoteWindow(IQuotesViewModel quotesViewModel)
        {
            InitializeComponent();
            QuotesView.DataContext = quotesViewModel;
        }
    }
}