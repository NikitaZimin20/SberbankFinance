using SberbankFinance.Commands;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class IncomeViewModel:ChartView
    {
        
        public IncomeViewModel():base(States.BalanceState.Income)
        {
          
        }
    }
}
