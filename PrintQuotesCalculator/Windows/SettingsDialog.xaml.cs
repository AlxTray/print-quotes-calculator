using System.Windows;
using PrintQuotesCalculator.ViewModels;

namespace PrintQuotesCalculator.Windows
{
    public partial class SettingsDialog : Window
    {
        internal SettingsDialog(ISettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            SettingsView.DataContext = settingsViewModel;
        }
    }
}
