using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Validation
{
    internal static class ValidationExtenthion
    {
        public static Validator Rules(this string field) => new Validator(field);
    }
}
