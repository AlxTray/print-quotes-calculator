using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace print_quotes_calculator.Utilities
{
    internal interface IQuoteContext 
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Ink> Inks { get; set; }
        public DbSet<Quote> Rows { get; set; }
    }
}
