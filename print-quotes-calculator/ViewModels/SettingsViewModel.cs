using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using print_quotes_calculator.Models;

namespace print_quotes_calculator.ViewModels
{
    internal class SettingsViewModel : ISettingsViewModel, INotifyPropertyChanged
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly ICsvWrapper _csvWrapper;
        private string _textBoxName;
        private decimal _textBoxCost;
        private string _selectedName;
        private Dictionary<string, decimal> _materials;
        private Dictionary<string, decimal> _inks;
        private Dictionary<string, decimal> _selectedCollection;
        private bool _materialIsChecked;
        private bool _inkIsChecked;

        public SettingsViewModel(IDatabaseHelper databaseHelper, ICsvWrapper csvWrapper)
        {
            _databaseHelper = databaseHelper;
            _csvWrapper = csvWrapper;

            _materials = _databaseHelper.GetMaterials();
            _inks = _databaseHelper.GetInks();

            AddMaterialOrInkCommand = new RelayCommand(AddMaterialOrInk);
            RemoveMaterialOrInkCommand = new RelayCommand(RemoveMaterialOrInk);
            WriteMaterialsOrInksCommand = new RelayCommand(WriteMaterialsOrInks);
            ReadMaterialsOrInksCommand = new RelayCommand(ReadMaterialsOrInks);
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

        public Dictionary<string, decimal> SelectedCollection
        {
            get => _selectedCollection;
            set
            {
                _selectedCollection = value;
                if (MaterialIsChecked)
                {
                    _materials = value;
                }
                else
                {
                    _inks = value;
                }
                RaisePropertyChanged(nameof(SelectedCollection));
            }
        }

        public bool MaterialIsChecked
        {
            get => _materialIsChecked;
            set
            {
                _materialIsChecked = value;
                _inkIsChecked = !value;
                if (value) SelectedCollection = _materials;
                RaisePropertyChanged(nameof(MaterialIsChecked));
            }
        }

        public bool InkIsChecked
        {
            get => _inkIsChecked;
            set
            {
                _inkIsChecked = value;
                _materialIsChecked = !value;
                if (value) SelectedCollection = _inks;
                RaisePropertyChanged(nameof(InkIsChecked));
            }
        }


        public ICommand AddMaterialOrInkCommand { get; }

        public void AddMaterialOrInk()
        {
            if (MaterialIsChecked)
            {
                _databaseHelper.AddMaterial(TextBoxName, TextBoxCost);
                SelectedCollection = _databaseHelper.GetMaterials();
            }
            else
            {
                _databaseHelper.AddInk(TextBoxName, TextBoxCost);
                SelectedCollection = _databaseHelper.GetInks();
            }
        }


        public ICommand RemoveMaterialOrInkCommand { get; }

        public void RemoveMaterialOrInk()
        {
            if (MaterialIsChecked)
            {
                _databaseHelper.RemoveMaterial(SelectedName);
                SelectedCollection = _databaseHelper.GetMaterials();
            }
            else
            {
                _databaseHelper.RemoveInk(SelectedName);
                SelectedCollection = _databaseHelper.GetInks();
            }
        }


        public ICommand WriteMaterialsOrInksCommand { get; }

        public void WriteMaterialsOrInks()
        {
            var saveDialog = new SaveFileDialog
            {
                FileName = "Quotes",
                DefaultExt = ".csv",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = saveDialog.ShowDialog();
            if (result != true) return;

            if (MaterialIsChecked)
            {
                _csvWrapper.WriteMaterials(saveDialog.FileName, SelectedCollection);
            }
            else
            {
                _csvWrapper.WriteInks(saveDialog.FileName, SelectedCollection);
            }
        }


        public ICommand ReadMaterialsOrInksCommand { get; }

        public void ReadMaterialsOrInks()
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Select a Quotes CSV file",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = openDialog.ShowDialog();
            if (result != true) return;

            if (MaterialIsChecked)
            {
                SelectedCollection = _csvWrapper.ReadMaterials(openDialog.FileName, SelectedCollection);
            }
            else
            {
                SelectedCollection = _csvWrapper.ReadInks(openDialog.FileName, SelectedCollection);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
