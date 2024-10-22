using print_quotes_calculator.ViewModel;
using System.Windows;
using Unity;

namespace print_quotes_calculator
{
    public partial class QuoteWindow : Window
    {
        public QuoteWindow(UnityContainer container)
        {
            InitializeComponent();
            this.quotesView.DataContext = container.Resolve<QuotesViewModel>();
        }
    }
}