using System.ComponentModel;
using print_quotes_calculator.Model;
using Unity;

namespace print_quotes_calculator.ViewModel
{
    internal class QuoteRow(UnityContainer container) : IQuoteRow, INotifyPropertyChanged
    {
        private string _material = string.Empty;
        private decimal _materialUsage;
        private string _ink = string.Empty;
        private decimal _inkUsage;
        private string _description = string.Empty;
        private decimal _quoteCost = 0.00m;

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
        public decimal MaterialUsage
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
        public decimal InkUsage
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
        public decimal QuoteCost
        {
            get => _quoteCost;
            set
            {
                _quoteCost = value;
                RaisePropertyChanged(nameof(QuoteCost));
            }
        }
        public void CalculateQuoteCost()
        {
            var quoteViewModel = container.Resolve<QuotesViewModel>();
            decimal materialCost;
            decimal inkCost;
            try
            {
                materialCost = quoteViewModel.MaterialTypes[Material];
                inkCost = quoteViewModel.InkTypes[Ink];
            }
            catch (KeyNotFoundException e)
            {
                QuoteCost = 0.00m;
                return;
            }
            QuoteCost = container.Resolve<QuoteCalculator>().CalculateQuote(MaterialUsage, materialCost, InkUsage, inkCost);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
