using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace print_quotes_calculator.Utilities
{
    internal class DatabaseHelper : IDatabaseHelper
    {
        public Dictionary<string, decimal> GetInks()
        {
            return [];
        }

        public Dictionary<string, decimal> GetMaterials()
        {
            return [];
        }
    }
}
