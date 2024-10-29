using System.Windows;
using print_quotes_calculator.ViewModels;

namespace print_quotes_calculator.Windows
{
    public partial class SettingsDialog : Window
    {
        internal SettingsDialog(ISettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            settingsView.DataContext = settingsViewModel;
        }
    }
}
