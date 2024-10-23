using System.Windows;
using print_quotes_calculator.ViewModels;
using Unity;

namespace print_quotes_calculator.Windows
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