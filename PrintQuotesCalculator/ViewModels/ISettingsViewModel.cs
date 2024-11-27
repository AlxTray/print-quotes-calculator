using System.Windows.Input;

namespace PrintQuotesCalculator.ViewModels
{
    internal interface ISettingsViewModel
    {
        string TextBoxName { get; set; }
        decimal TextBoxCost { get; set; }
        string SelectedName { get; set; }
        Dictionary<string, decimal> SelectedCollection { get; set; }
        bool MaterialIsChecked { get; set; }
        bool InkIsChecked { get; set; }
        ICommand AddMaterialOrInkCommand { get; }
        void AddMaterialOrInk();
        ICommand RemoveMaterialOrInkCommand { get; }
        void RemoveMaterialOrInk();
    }
}
