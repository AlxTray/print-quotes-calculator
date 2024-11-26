using print_quotes_calculator.ViewModels;
using print_quotes_calculator.Windows;

namespace print_quotes_calculator.Utilities
{
    class SettingsDialogFactory(ISettingsViewModel settingsViewModel) : ISettingsDialogFactory
    {
        public void Create()
        {
            new SettingsDialog(settingsViewModel).ShowDialog();
        }
    }
}
