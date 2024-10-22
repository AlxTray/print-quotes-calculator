
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
        private string _textBoxName;
        private decimal _textBoxCost;
        private string _selectedName;
        private Dictionary<string, decimal> _materials;
        private Dictionary<string, decimal> _inks;
        private Dictionary<string, decimal> _selectedCollection;
        private bool _materialIsChecked;
        private bool _inkIsChecked;

        public SettingsViewModel(UnityContainer container)
        {
            _container = container;
            var db = _container.Resolve<DatabaseHelper>();
            _materials = db.GetMaterials();
            _inks = db.GetInks();

            AddMaterialOrInkCommand = new RelayCommand(AddMaterialOrInk);
            RemoveMaterialOrInkCommand = new RelayCommand(RemoveMaterialOrInk);
        }

        public string TextBoxName
        {
            get => _textBoxName;
            set
            {
                _textBoxName = value;
                RaisePropertyChanged(nameof(TextBoxName));
            }
        }

        public decimal TextBoxCost
        {
            get => _textBoxCost;
            set
            {
                _textBoxCost = value;
                RaisePropertyChanged(nameof(TextBoxCost));
            }
        }

        public string SelectedName
        {
            get => _selectedName;
            set
            {
                _selectedName = value;
                RaisePropertyChanged(nameof(SelectedName));
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

        public Dictionary<string, decimal> SelectedCollection
        {
            get => _selectedCollection;
            set
            {
                _selectedCollection = value;
                RaisePropertyChanged(nameof(SelectedCollection));
            }
        }

        public bool MaterialIsChecked
        {
            get => _materialIsChecked;
            set
            {
                _materialIsChecked = value;
                if (value) SelectedCollection = Materials;
                RaisePropertyChanged(nameof(MaterialIsChecked));
            }
        }

        public bool InkIsChecked
        {
            get => _inkIsChecked;
            set
            {
                _inkIsChecked = value;
                if (value) SelectedCollection = Inks;
                RaisePropertyChanged(nameof(InkIsChecked));
            }
        }


        public ICommand AddMaterialOrInkCommand { get; }

        public void AddMaterialOrInk()
        {
            var db = _container.Resolve<DatabaseHelper>();
            if (MaterialIsChecked)
            {
                db.AddMaterial(TextBoxName, TextBoxCost);
                SelectedCollection = Materials = db.GetMaterials();
            }
            else
            {
                db.AddInk(TextBoxName, TextBoxCost);
                SelectedCollection = Inks = db.GetInks();
            }
        }


        public ICommand RemoveMaterialOrInkCommand { get; }

        public void RemoveMaterialOrInk()
        {
            var db = _container.Resolve<DatabaseHelper>();
            if (MaterialIsChecked)
            {
                db.RemoveMaterial(SelectedName);
                SelectedCollection = Materials = db.GetMaterials();
            }
            else
            {
                db.RemoveInk(SelectedName);
                SelectedCollection = Inks = db.GetInks();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
