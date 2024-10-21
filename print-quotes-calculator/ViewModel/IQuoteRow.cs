using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace print_quotes_calculator.ViewModel
{
    internal interface IQuoteRow
    {
        string Material { get; set; }
        decimal MaterialUsage { get; set; }
        string Ink { get; set; }
        decimal InkUsage { get; set; }
        string Description { get; set; }
        decimal QuoteCost { get; set; }
        void CalculateQuoteCost();
    }
}
