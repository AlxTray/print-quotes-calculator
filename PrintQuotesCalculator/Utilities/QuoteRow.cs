﻿using System.ComponentModel;

namespace PrintQuotesCalculator.Utilities
{
    internal class QuoteRow(long id) : IQuoteRow, INotifyPropertyChanged
    {
        private string _material = string.Empty;
        private decimal _materialUsage;
        private string _ink = string.Empty;
        private decimal _inkUsage;
        private string _description = string.Empty;
        private decimal _quoteCost = 0.00m;

        // Needed for when reading the CSV by CSVHelper,
        // so that the id constructor is first 0 and then populated by the Id property
        public QuoteRow() : this(0) { }

        public long Id { get; set; } = id;

        public string Material
        {
            get => _material;
            set
            {
                _material = value;
                RaisePropertyChanged(nameof(Material));
            }
        }

        public decimal MaterialUsage
        {
            get => _materialUsage;
            set
            {
                _materialUsage = value;
                RaisePropertyChanged(nameof(MaterialUsage));
            }
        }

        public string Ink
        {
            get => _ink;
            set
            {
                _ink = value;
                RaisePropertyChanged(nameof(Ink));
            }
        }

        public decimal InkUsage
        {
            get => _inkUsage;
            set
            {
                _inkUsage = value;
                RaisePropertyChanged(nameof(InkUsage));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public decimal QuoteCost
        {
            get => _quoteCost;
            set
            {
                _quoteCost = value;
                RaisePropertyChanged(nameof(QuoteCost));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
