using System.ComponentModel;
using System.Windows;
using print_quotes_calculator.ViewModel;
using print_quotes_calculator.ViewModels;
using Unity;

namespace print_quotes_calculator.Windows
{
    public partial class SettingsDialog : Window
    {
        public SettingsDialog(UnityContainer container)
        {
            InitializeComponent();
            this.settingsView.DataContext = container.Resolve<SettingsViewModel>();
        }
    }
}
