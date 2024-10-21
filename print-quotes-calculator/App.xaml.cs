using print_quotes_calculator.ViewModel;
using System.Windows;
using print_quotes_calculator.Model;
using print_quotes_calculator.Utilities;
using Unity;
using Unity.Lifetime;

namespace print_quotes_calculator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();
            container.RegisterType<IDatabaseHelper, DatabaseHelper>();
            container.RegisterType<IQuoteCalculator, QuoteCalculator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IQuotesViewModel, QuotesViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IQuoteRow, QuoteRow>();
            container.RegisterType<MainWindow>();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }

}
