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
       
       public LiveCharts.Wpf.PieChart Series { get; set; }
        public string[] TypeOfWastes { get; set;}
        private void InitializeChart()
        {
           SeriesCollection psc = new SeriesCollection
         {
        new LiveCharts.Wpf.PieSeries
        {
            
        Values = new LiveCharts.ChartValues<decimal> {1,1},
        },
        new LiveCharts.Wpf.PieSeries
        {
        Values = new LiveCharts.ChartValues<decimal> {3,7},
        }
            };
            foreach (LiveCharts.Wpf.PieSeries ps in psc)
            {
                Series.Series.Add(ps);
            }

        }
        public OutcomeViewModel()
        {
            Series = new LiveCharts.Wpf.PieChart();
            InitializeChart();
            
            TypeOfWastes = new string[] {"Хуй","Залупа" };
        }
      
    }
}
