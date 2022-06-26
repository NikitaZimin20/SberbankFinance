using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Model
{
    internal class ErrorsModel
    {
        public string LessSymbols { get; set; }
        public string MoreSymbols { get; set; }
        public string IncorrectSymbols { get; set; }

        public string IncorrectPassword { get; set; }
    }
}
