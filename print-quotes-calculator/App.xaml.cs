using print_quotes_calculator.ViewModels;
using System.Windows;
using print_quotes_calculator.Models;
using print_quotes_calculator.Utilities;
using print_quotes_calculator.Windows;
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
            container.RegisterType<IQuoteContext, QuoteContext>();
            container.RegisterType<IDatabaseHelper, DatabaseHelper>();
            container.RegisterType<IQuoteCalculator, QuoteCalculator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IQuotesViewModel, QuotesViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IQuoteRow, QuoteRow>();
            container.RegisterType<QuoteWindow>();
            container.RegisterType<ISettingsViewModel, SettingsViewModel>();
            container.RegisterType<SettingsDialog>();

            var mainWindow = container.Resolve<QuoteWindow>();
            mainWindow.Show();
        }
    }

}
