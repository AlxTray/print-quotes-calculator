using PrintQuotesCalculator.ViewModels;
using PrintQuotesCalculator.Windows;

namespace PrintQuotesCalculator.Utilities
{
    class SettingsDialogFactory(ISettingsViewModel settingsViewModel) : ISettingsDialogFactory
    {
        public void Create()
        {
            new SettingsDialog(settingsViewModel).ShowDialog();
        }
    }
}
