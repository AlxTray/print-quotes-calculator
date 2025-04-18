using System.Windows.Input;

namespace PrintQuotesCalculator.ViewModels
{
    internal interface ISettingsViewModel
    {
        string InkName { get; set; }
        decimal InkCost { get; set; }
        string MaterialName { get; set; }
        decimal MaterialCost { get; set; }
        string SelectedInk { get; set; }
        string SelectedMaterial { get; set; }
        IDictionary<string, decimal> Inks { get; set; }
        IDictionary<string, decimal> Materials { get; set; }
        ICommand AddInkCommand { get; }
        void AddInk();
        ICommand AddMaterialCommand { get; }
        void AddMaterial();
        ICommand RemoveInkCommand { get; }
        void RemoveInk();
        ICommand RemoveMaterialCommand { get; }
        void RemoveMaterial();
    }
}
