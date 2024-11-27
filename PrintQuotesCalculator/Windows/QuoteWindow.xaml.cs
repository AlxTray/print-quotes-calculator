using System.Windows;
using PrintQuotesCalculator.ViewModels;

namespace PrintQuotesCalculator.Windows
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