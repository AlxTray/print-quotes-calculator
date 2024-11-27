using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PrintQuotesCalculator.Models;
using PrintQuotesCalculator.Utilities;

namespace PrintQuotesCalculator.ViewModels
{
    internal class QuotesViewModel : IQuotesViewModel, INotifyPropertyChanged
    {
        private readonly IDatabaseHelper _db;
        private readonly IQuoteCalculator _quoteCalculator;
        private readonly ICsvWrapper _csvWrapper;
        private readonly ISettingsDialogFactory _settingsDialogFactory;
        private ObservableCollection<QuoteRow> _quoteRows;
        private IList _selectedRows;
        private Dictionary<string, decimal> _materials;
        private Dictionary<string, decimal> _inks;
        private decimal _totalQuotesCost;

        public QuotesViewModel(IDatabaseHelper db, IQuoteCalculator quoteCalculator, ICsvWrapper csvWrapper, ISettingsDialogFactory settingsDialogFactory)
        {
            _db = db;
            _quoteCalculator = quoteCalculator;
            _csvWrapper = csvWrapper;
            _settingsDialogFactory = settingsDialogFactory;

            _materials = _db.GetMaterials();
            _inks = _db.GetInks();

            _quoteRows = [];
            // This does cause it calling the AddOrUpdateQuoteRow()
            // function on each add but this will just update each row with the same values.
            // CollectionChanged needs to be added before so each row gets the PropertyChanged event added
            _quoteRows.CollectionChanged += QuoteRow_CollectionChanged;
            foreach (QuoteRow row in db.GetQuoteRows())
            {
                _quoteRows.Add(row);
            }

            NewCommand = new RelayCommand(NewQuotesCheck);
            AddCommand = new RelayCommand(AddQuoteRow);
            RemoveCommand = new RelayCommand(RemoveSelectedQuoteRows);
            ShowSettingsDialogCommand = new RelayCommand(ShowSettingsDialog);
            SaveCommand = new RelayCommand(SaveQuoteRows);
            OpenCommand = new RelayCommand(OpenQuoteRows);
            AppendCommand = new RelayCommand(AppendQuoteRows);
        }

        public ObservableCollection<QuoteRow> QuoteRows
        {
            get => _quoteRows;
            set
            {
                _quoteRows = value;
                RaisePropertyChanged(nameof(QuoteRows));
            }
        }

        public IList SelectedRows
        {
            get => _selectedRows;
            set
            {
                // When nothing is selected SelectedItems in BindableSelectionDataGrid is null,
                // therefore initialise with default ArrayList when it is null to have a Count of 0 displayed when nothing is selected
                // and does not explode when attempting to remove with nothing selected
                if (value == null)
                {
                    _selectedRows = new ArrayList();
                    return;
                }
                _selectedRows = value;
                RaisePropertyChanged(nameof(SelectedRows));
            }
        }

        public Dictionary<string, decimal> MaterialTypes
        {
            get => _materials;
            set
            {
                _materials = value;
                RaisePropertyChanged(nameof(MaterialTypes));
            }
        }

        public Dictionary<string, decimal> InkTypes
        {
            get => _inks;
            set
            {
                _inks = value;
                RaisePropertyChanged(nameof(MaterialTypes));
            }
        }

        public decimal TotalQuotesCost
        {
            get => _totalQuotesCost;
            set
            {
                _totalQuotesCost = value;
                RaisePropertyChanged(nameof(TotalQuotesCost));
            }
        }


        public ICommand NewCommand { get; }

        public void NewQuotesCheck()
        {
            var result = MessageBox.Show("Are you sure you want to delete all quotes from local storage?",
                "Remove quotes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) ClearQuoteRows();
        }


        public ICommand OpenCommand { get; }

        public void OpenQuoteRows()
        {
            ClearQuoteRows();
            AppendQuoteRows();
        }


        public void ClearQuoteRows()
        {
            // Remove each item one by one as .Clear() does not fire CollectionChanged event
            foreach (var row in QuoteRows.ToList())
            {
                QuoteRows.Remove(row);
            }
        }


        public ICommand AppendCommand { get; }

        public void AppendQuoteRows()
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Select a Quotes CSV file",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = openDialog.ShowDialog();
            if (result != true) return;
            QuoteRows = _csvWrapper.ReadQuotes(openDialog.FileName, QuoteRows);
        }


        public ICommand AddCommand { get; }

        public void AddQuoteRow()
        {
            long quoteId = 1;
            if (QuoteRows.Count > 0)
            {
                quoteId = QuoteRows.Last().Id + 1;
            }
            QuoteRows.Add(new QuoteRow(quoteId));
        }


        public ICommand RemoveCommand { get; }

        public void RemoveSelectedQuoteRows()
        {
            if (SelectedRows.Count == 0)
            {
                MessageBox.Show("No quotes have been selected to remove", "No quotes selected", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                return;
            }

            var quotePluralString = (SelectedRows.Count == 1) ? "quote" : "quotes";
            var result = MessageBox.Show($"Are you sure you want to delete {SelectedRows.Count} {quotePluralString} from local storage?",
                "Remove quotes", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;

            // Cannot be foreach as when row gets removed the selection is updated in the DataGrid,
            // modifying SelectedRows which stops iteration
            while (SelectedRows.Count != 0)
            {
                // As above, removing from QuoteRows will also remove from SelectedRows so just get first item until list is empty
                QuoteRows.Remove((QuoteRow)SelectedRows[0]);
            }
        }


        public ICommand ShowSettingsDialogCommand { get; }

        public void ShowSettingsDialog()
        {
            _settingsDialogFactory.Create();
            MaterialTypes = _db.GetMaterials();
            InkTypes = _db.GetInks();
        }


        public ICommand SaveCommand { get; }

        public void SaveQuoteRows()
        {
            var saveDialog = new SaveFileDialog
            {
                FileName = "Quotes",
                DefaultExt = ".csv",
                Filter = "CSV Files(*.csv)|*.csv"
            };

            var result = saveDialog.ShowDialog();
            if (result == true) _csvWrapper.WriteQuotes(saveDialog.FileName, QuoteRows);
        }


        public void QuoteRow_CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.NewItems != null)
            {
                foreach (QuoteRow quoteRow in eventArgs.NewItems)
                {
                    quoteRow.PropertyChanged += QuoteRow_PropertyChanged;
                    if (quoteRow.QuoteCost == 0) continue;
                    _db.AddOrUpdateQuoteRow(quoteRow);
                    TotalQuotesCost += quoteRow.QuoteCost;
                }
            }

            if (eventArgs.OldItems != null)
            {
                foreach (QuoteRow quoteRow in eventArgs.OldItems)
                {
                    quoteRow.PropertyChanged -= QuoteRow_PropertyChanged;
                    _db.RemoveQuoteRow(quoteRow);
                    TotalQuotesCost -= quoteRow.QuoteCost;
                }
            }
        }

        public void QuoteRow_PropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (sender is not QuoteRow quoteRow) return;
            // Will have infinite recursion if QuoteCost is set here, which causes a PropertyChanged event
            if (eventArgs.PropertyName is "QuoteCost") return;

            decimal materialCost;
            decimal inkCost;
            try
            {
                materialCost = MaterialTypes[quoteRow.Material];
                inkCost = InkTypes[quoteRow.Ink];
            }
            catch (KeyNotFoundException e)
            {
                quoteRow.QuoteCost = 0.00m;
                return;
            }

            var previousCost = quoteRow.QuoteCost;
            quoteRow.QuoteCost = _quoteCalculator.CalculateQuote(quoteRow.MaterialUsage, materialCost, quoteRow.InkUsage, inkCost);
            TotalQuotesCost -= previousCost;
            TotalQuotesCost += quoteRow.QuoteCost;

            _db.AddOrUpdateQuoteRow(quoteRow);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
