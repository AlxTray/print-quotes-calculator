﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using print_quotes_calculator.Models;
using print_quotes_calculator.Utilities;
using print_quotes_calculator.Windows;

namespace print_quotes_calculator.ViewModels
{
    internal class QuotesViewModel : IQuotesViewModel, INotifyPropertyChanged
    {
        private readonly IDatabaseHelper _db;
        private readonly IQuoteCalculator _quoteCalculator;
        private readonly SettingsDialog _settingsDialog;
        private ObservableCollection<QuoteRow> _quoteRows;
        private Dictionary<string, decimal> _materials;
        private Dictionary<string, decimal> _inks;
        private decimal _totalQuotesCost;

        public QuotesViewModel(IDatabaseHelper db, IQuoteCalculator quoteCalculator, SettingsDialog settingsDialog)
        {
            _db = db;
            _quoteCalculator = quoteCalculator;
            _settingsDialog = settingsDialog;

            _materials = _db.GetMaterials();
            _inks = _db.GetInks();

            _quoteRows = [];
            _quoteRows.CollectionChanged += QuoteRow_CollectionChanged;
            foreach (QuoteRow row in db.GetQuoteRows())
            {
                _quoteRows.Add(row);
            }
            CalculateTotalCost();

            AddCommand = new RelayCommand(AddQuoteRow);
            ShowSettingsDialogCommand = new RelayCommand(ShowSettingsDialog);
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


        public ICommand AddCommand { get; }

        public void AddQuoteRow()
        {
            long quoteId = 1;
            if (_quoteRows.Count > 0)
            {
                quoteId = _quoteRows.Last().Id + 1;
            }
            _quoteRows.Add(new QuoteRow(quoteId));
        }

        public ICommand ShowSettingsDialogCommand { get; }

        public void ShowSettingsDialog()
        {
            _settingsDialog.ShowDialog();
            MaterialTypes = _db.GetMaterials();
            InkTypes = _db.GetInks();
        }


        private void CalculateTotalCost()
        {
            TotalQuotesCost = _quoteRows.Sum(row => row.QuoteCost);
        }


        private void QuoteRow_CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.NewItems != null)
            {
                foreach (QuoteRow quoteRow in eventArgs.NewItems)
                {
                    quoteRow.PropertyChanged += QuoteRow_PropertyChanged;
                }
            }

            if (eventArgs.OldItems != null)
            {
                foreach (QuoteRow quoteRow in eventArgs.OldItems)
                {
                    quoteRow.PropertyChanged -= QuoteRow_PropertyChanged;
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
            quoteRow.QuoteCost = _quoteCalculator.CalculateQuote(quoteRow.MaterialUsage, materialCost, quoteRow.InkUsage, inkCost);
            CalculateTotalCost();

            _db.AddOrUpdateQuoteRow(quoteRow);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
