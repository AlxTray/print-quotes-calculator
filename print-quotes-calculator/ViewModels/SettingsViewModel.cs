
using System.ComponentModel;
using Unity;

namespace print_quotes_calculator.ViewModels
{
    internal class SettingsViewModel : ISettingsViewModel, INotifyPropertyChanged
    {
        private readonly UnityContainer _container;
        private string _materialName;
        private decimal _materialCost;
        private string _inkName;
        private decimal _inkCost;
        private Dictionary<string, decimal> _materials;
        private Dictionary<string, decimal> _inks;

        public SettingsViewModel(UnityContainer container)
        {
            _container = container;
            _materials = new Dictionary<string, decimal>();
            _inks = new Dictionary<string, decimal>();
        }

        public string MaterialName
        {
            get => _materialName;
            set
            {
                _materialName = value;
                RaisePropertyChanged(nameof(MaterialName));
            }
        }

        public decimal MaterialCost
        {
            get => _materialCost;
            set
            {
                _materialCost = value;
                RaisePropertyChanged(nameof(MaterialCost));
            }
        }

        public string InkName
        {
            get => _inkName;
            set
            {
                _inkName = value;
                RaisePropertyChanged(nameof(InkName));
            }
        }

        public decimal InkCost
        {
            get => _inkCost;
            set
            {
                _inkCost = value;
                RaisePropertyChanged(nameof(InkCost));
            }
        }

        public Dictionary<string, decimal> Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                RaisePropertyChanged(nameof(Materials));
            }
        }

        public Dictionary<string, decimal> Inks
        {
            get => _inks;
            set
            {
                _inks = value;
                RaisePropertyChanged(nameof(Inks));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
