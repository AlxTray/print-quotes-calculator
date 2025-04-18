using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using Microsoft.Win32;
using PrintQuotesCalculator.Models;

namespace PrintQuotesCalculator.ViewModels
{
    internal class SettingsViewModel : ISettingsViewModel, INotifyPropertyChanged
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly ICsvWrapper _csvWrapper;
        private string _inkName;
        private decimal _inkCost;
        private string _materialName;
        private decimal _materialCost;
        private string _selectedInk;
        private string _selectedMaterial;
        private IDictionary<string, decimal> _materials;
        private IDictionary<string, decimal> _inks;

        public SettingsViewModel(IDatabaseHelper databaseHelper, ICsvWrapper csvWrapper)
        {
            _databaseHelper = databaseHelper;
            _csvWrapper = csvWrapper;

            _materials = _databaseHelper.GetMaterials();
            _inks = _databaseHelper.GetInks();

            AddInkCommand = new RelayCommand(AddInk);
            AddMaterialCommand = new RelayCommand(AddMaterial);
            RemoveInkCommand = new RelayCommand(RemoveInk);
            RemoveMaterialCommand = new RelayCommand(RemoveMaterial);
            WriteInksCommand = new RelayCommand(WriteInks);
            WriteMaterialsCommand = new RelayCommand(WriteMaterials);
            ReadInksCommand = new RelayCommand(ReadInks);
            ReadMaterialsCommand = new RelayCommand(ReadMaterials);
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

        public string SelectedInk
        {
            get => _selectedInk;
            set
            {
                _selectedInk = value;
                RaisePropertyChanged(nameof(SelectedInk));
            }
        }

        public string SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                RaisePropertyChanged(nameof(SelectedMaterial));
            }
        }

        public IDictionary<string, decimal> Inks
        {
            get => _inks;
            set
            {
                _inks = value;
                RaisePropertyChanged(nameof(Inks));
            }
        }

        public IDictionary<string, decimal> Materials
        {
            get => _materials;
            set
            {
                _materials = value;
                RaisePropertyChanged(nameof(Materials));
            }
        }

        public ICommand AddInkCommand { get; }
        public void AddInk()
        {
            if (InkName == string.Empty)
            {
                MessageBox.Show("Insufficient details provided to add new ink", "Insufficient details", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }
            if (InkCost == 0)
            {
                var result = MessageBox.Show("Cost for new ink is zero. Are you sure?", "Insufficient details", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No) return;
            }

            _databaseHelper.AddInk(InkName, InkCost);
            Inks = _databaseHelper.GetInks();
            InkName = string.Empty;
            InkCost = 0;
        }

        public ICommand AddMaterialCommand { get; }
        public void AddMaterial()
        {
            if (MaterialName == string.Empty)
            {
                MessageBox.Show("Insufficient details provided to add new material", "Insufficient details", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }
            if (MaterialCost == 0)
            {
                var result = MessageBox.Show("Cost for new material is zero. Are you sure?", "Insufficient details", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No) return;
            }

            _databaseHelper.AddMaterial(MaterialName, MaterialCost);
            Materials = _databaseHelper.GetMaterials();
            MaterialName = string.Empty;
            MaterialCost = 0;
        }

        public ICommand RemoveInkCommand { get; }
        public void RemoveInk()
        {
            if (SelectedInk == string.Empty)
            {
                MessageBox.Show("Please select an existing ink to remove", $"No ink selected", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }

            _databaseHelper.RemoveInk(SelectedInk);
            Inks = _databaseHelper.GetInks();
        }

        public ICommand RemoveMaterialCommand { get; }
        public void RemoveMaterial()
        {
            if (SelectedMaterial == string.Empty)
            {
                MessageBox.Show("Please select an existing material to remove", $"No material selected", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }

            _databaseHelper.RemoveMaterial(SelectedMaterial);
            Materials = _databaseHelper.GetMaterials();
        }


        public ICommand WriteInksCommand { get; }
        public void WriteInks()
        {
            var saveDialog = new SaveFileDialog
            {
                FileName = "inks",
                DefaultExt = ".csv",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = saveDialog.ShowDialog();
            if (!result.HasValue || !result.Value) return;

            _csvWrapper.WriteInks(saveDialog.FileName, Inks);
        }

        public ICommand WriteMaterialsCommand { get; }
        public void WriteMaterials()
        {
            var saveDialog = new SaveFileDialog
            {
                FileName = "materials",
                DefaultExt = ".csv",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = saveDialog.ShowDialog();
            if (!result.HasValue || !result.Value) return;

            _csvWrapper.WriteMaterials(saveDialog.FileName, Materials);
        }

        public ICommand ReadInksCommand { get; }
        public void ReadInks()
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Select an inks CSV file",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = openDialog.ShowDialog();
            if (!result.HasValue || !result.Value) return;

            try
            {
                Inks = _csvWrapper.ReadInks(openDialog.FileName, Inks);
            }
            catch (HeaderValidationException e)
            {
                MessageBox.Show("Invalid CSV file selected, please try another.", "Invalid CSV", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
            }
        }

        public ICommand ReadMaterialsCommand { get; }
        public void ReadMaterials()
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Select a materials CSV file",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = openDialog.ShowDialog();
            if (!result.HasValue || !result.Value) return;

            try
            {
                Materials = _csvWrapper.ReadMaterials(openDialog.FileName, Materials);
            }
            catch (HeaderValidationException e)
            {
                MessageBox.Show("Invalid CSV file selected, please try another.", "Invalid CSV", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
