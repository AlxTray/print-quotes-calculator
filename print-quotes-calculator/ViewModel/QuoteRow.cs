using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace print_quotes_calculator.ViewModel
{
    class QuoteRow : IQuoteRow, INotifyPropertyChanged
    {
        private string _material;
        private double _materialUsage;
        private string _ink;
        private double _inkUsage;
        private string _description;
        private double _quoteCost;

        public string Material 
        { 
            get => _material;
            set 
            { 
                _material = value;
                CalculateQuoteCost();
                RaisePropertyChanged(nameof(Material));
            } 
        }
        public double MaterialUsage 
        { 
            get => _materialUsage;
            set 
            { 
                _materialUsage = value;
                CalculateQuoteCost();
                RaisePropertyChanged(nameof(MaterialUsage));
            }
        }
        public string Ink 
        { 
            get => _ink;
            set
            {
                _ink = value;
                CalculateQuoteCost();
                RaisePropertyChanged(nameof(Ink));
            }
        }
        public double InkUsage 
        { 
            get => _inkUsage;
            set
            {
                _inkUsage = value;
                CalculateQuoteCost();
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
        public double QuoteCost 
        { 
            get => _quoteCost;
            set
            {
                _quoteCost = value;
                RaisePropertyChanged(nameof(QuoteCost));
            }
        }
        private void CalculateQuoteCost()
        {
            QuoteCost = _materialUsage + _inkUsage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
