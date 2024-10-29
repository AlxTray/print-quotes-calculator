using System.Windows.Input;

namespace print_quotes_calculator.ViewModels
{
    internal interface ISettingsViewModel
    {
        string TextBoxName { get; set; }
        decimal TextBoxCost { get; set; }
        string SelectedName { get; set; }
        Dictionary<string, decimal> Materials { get; set; }
        Dictionary<string, decimal> Inks { get; set; }
        Dictionary<string, decimal> SelectedCollection { get; set; }
        bool MaterialIsChecked { get; set; }
        bool InkIsChecked { get; set; }
        ICommand AddMaterialOrInkCommand { get; }
        void AddMaterialOrInk();
        ICommand RemoveMaterialOrInkCommand { get; }
        void RemoveMaterialOrInk();
    }
}
