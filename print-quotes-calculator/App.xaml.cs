using print_quotes_calculator.ViewModel;
using System.Windows;
using Unity;

namespace print_quotes_calculator
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new UnityContainer();
            container.RegisterType<QuotesViewModel>();
            container.RegisterType<MainWindow>();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }

}
