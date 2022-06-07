using LiveCharts;
using SberbankFinance.Commands;
using SberbankFinance.Model;
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

        public string[] TypeOfWastes { get; set;}
       
        public OutcomeViewModel()
        {
                 
            TypeOfWastes = new string[] {"Хуй","Залупа" };
        }
      
    }
}
