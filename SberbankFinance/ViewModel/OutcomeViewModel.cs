using LiveCharts;
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
    internal class OutcomeViewModel:BaseViewModel
    {
        public ChartValues<int> Cases { get; set;}
        public string[] TypeOfWastes { get; set;}
        public OutcomeViewModel()
        {
            Cases = new ChartValues<int>
            {
                5,12,223,232,232,231,442
            };
            TypeOfWastes = new string[] {"Хуй","Залупа" };
        }
      
    }
}
