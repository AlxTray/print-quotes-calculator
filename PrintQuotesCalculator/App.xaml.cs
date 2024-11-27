using System.Windows;
using PrintQuotesCalculator.Models;
using PrintQuotesCalculator.Utilities;
using PrintQuotesCalculator.ViewModels;
using PrintQuotesCalculator.Windows;
using Unity;

namespace PrintQuotesCalculator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();
            container.RegisterType<IQuoteContext, QuoteContext>();
            container.RegisterType<IDatabaseHelper, DatabaseHelper>();
            container.RegisterType<IQuoteCalculator, QuoteCalculator>();
            container.RegisterType<ICsvWrapper, CsvWrapper>();
            container.RegisterType<IQuotesViewModel, QuotesViewModel>();
            container.RegisterType<QuoteWindow>();
            container.RegisterType<ISettingsDialogFactory, SettingsDialogFactory>();
            container.RegisterType<ISettingsViewModel, SettingsViewModel>();
            container.RegisterType<SettingsDialog>();

            var mainWindow = container.Resolve<QuoteWindow>();
            mainWindow.Show();
        }
    }

}
