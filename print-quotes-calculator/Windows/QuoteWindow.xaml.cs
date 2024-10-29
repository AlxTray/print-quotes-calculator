using System.Windows;
using print_quotes_calculator.ViewModels;
using Unity;

namespace print_quotes_calculator.Windows
{
    public partial class QuoteWindow : Window
    {
        internal QuoteWindow(IQuotesViewModel quotesViewModel)
        {
            InitializeComponent();
            quotesView.DataContext = quotesViewModel;
        }
    }
}