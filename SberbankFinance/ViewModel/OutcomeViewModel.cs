using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SberbankFinance.Commands;
using SberbankFinance.Model;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.States;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class OutcomeViewModel : ChartView
    {

        
        public OutcomeViewModel(NavigationStore navigationStore):base(navigationStore, BalanceState.Outcome)
        {
           
        }
      
    }
    } 
