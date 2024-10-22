
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using print_quotes_calculator.Models;
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
            var db = _container.Resolve<DatabaseHelper>();
            _materials = db.GetMaterials();
            _inks = db.GetInks();

            AddMaterialCommand = new RelayCommand(AddMaterial);
            AddInkCommand = new RelayCommand(AddInk);
            RemoveMaterialCommand = new RelayCommand(RemoveMaterial);
            RemoveInkCommand = new RelayCommand(RemoveInk);
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


        public ICommand AddMaterialCommand { get; }

        public void AddMaterial()
        {
            var db = _container.Resolve<DatabaseHelper>();
            db.AddMaterial(MaterialName, MaterialCost);
            Materials = db.GetMaterials();
        }

        public ICommand AddInkCommand { get; }

        public void AddInk()
        {
            var db = _container.Resolve<DatabaseHelper>();
            db.AddInk(InkName, InkCost);
            Inks = db.GetInks();
        }

        public ICommand RemoveMaterialCommand { get; }

        public void RemoveMaterial()
        {
            var db = _container.Resolve<DatabaseHelper>();
            db.RemoveMaterial(MaterialName);
            Materials = db.GetMaterials();
        }

        public ICommand RemoveInkCommand { get; }

        public void RemoveInk()
        {
            var db = _container.Resolve<DatabaseHelper>();
            db.RemoveInk(InkName);
            Inks = db.GetInks();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
