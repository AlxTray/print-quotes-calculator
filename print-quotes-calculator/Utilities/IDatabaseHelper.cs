using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace print_quotes_calculator.Utilities
{
    internal interface IDatabaseHelper 
    {
        Dictionary<string, decimal> GetMaterials();
        Dictionary<string, decimal> GetInks();
    }
}
