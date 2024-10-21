using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace print_quotes_calculator.Model
{
    internal interface IQuoteCalculator
    {
        decimal CalculateQuote(decimal materialUsage, decimal materialCost, decimal inkUsage, decimal inkCost);
    }
}
