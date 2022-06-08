using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SberbankFinance.Commands;
using SberbankFinance.Model;
using SberbankFinance.SqlDataAccess;
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
    internal class OutcomeViewModel:BaseViewModel
    {
        string _monthAmount;
        DateTime _selectedMonth = DateTime.Now;
        private SeriesCollection _seriesCol;

        public ICommand MonthReduceCommand { get;private set; } 
        private void OnExecuteMonthReduceCommand(object obj)
        {
            IsArrowEnable = true;
            SelectedMonth = _selectedMonth.AddMonths(-1);
            InsertData(_selectedMonth);
        }
        private bool CanExecuteMonthReduceCommand(object obj) => true;
        public bool IsArrowEnable
        {
            get
            {
                if (SelectedMonth.Month==DateTime.Now.Month)
                {
                    return false;
                }
                return true;
            }
            set
            {
                OnPropertyChanged(nameof(IsArrowEnable));
            }
        }
        public  SeriesCollection SeriesCol
        {
            get { return _seriesCol; }
            set
            {
                _seriesCol = value;
                OnPropertyChanged(nameof(SeriesCol));
            }
        }
       public DateTime SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth=value;
                MonthLable = _selectedMonth.ToString("MMM");
            }
        }
        public string MonthLable
        {
            get=>  _selectedMonth.ToString("MMM");
            set { OnPropertyChanged(nameof(MonthLable)); }
        } 
      
        public string MonthAmount
        {
            get=> _monthAmount;
            set
            {
                _monthAmount = value;
                OnPropertyChanged(nameof(MonthAmount));
            }
        } 
        
        private void InsertData(DateTime date)
        {
            double sum=0d;
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            var temp = new SeriesCollection();
            var sqlData = sql.GetOutcomeByMonth(Locator.Data.Id,_selectedMonth);
            foreach (var item in sqlData)
            {
                temp.Add(new PieSeries { Title = item.Categories, Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(item.Amount)) } });
                sum += double.Parse(item.Amount);
            }
            MonthAmount = sum.ToString();

            SeriesCol = temp;

        }
        public string[] TypeOfWastes { get; set;}
       
        public OutcomeViewModel()
        {
            MonthReduceCommand = new RelayCommand(OnExecuteMonthReduceCommand,CanExecuteMonthReduceCommand);
            SeriesCol = new SeriesCollection();
            InsertData(_selectedMonth);
            TypeOfWastes = new string[] {"Хуй","Залупа" };
        }
      
    }
}
