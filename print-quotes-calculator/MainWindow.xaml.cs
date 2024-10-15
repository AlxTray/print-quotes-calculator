using print_quotes_calculator.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace print_quotes_calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow(UnityContainer container)
        {
            InitializeComponent();
            this.quotesView.DataContext = container.Resolve<QuotesViewModel>();
        }
    }
}