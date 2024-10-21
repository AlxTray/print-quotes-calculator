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
        double MaterialUsage { get; set; }
        string Ink { get; set; }
        double InkUsage { get; set; }
        string Description { get; set; }
        double QuoteCost { get; set; }
        void CalculateQuoteCost();
    }
    }
}
