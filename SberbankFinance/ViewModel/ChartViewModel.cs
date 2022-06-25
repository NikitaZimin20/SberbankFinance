using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SberbankFinance.Commands;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.States;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SberbankFinance.ViewModel
{
    
    internal class ChartViewModel:BaseViewModel
    {
        string _monthAmount;
        private DateTime _selectedMonth = DateTime.Now;
        private SeriesCollection _seriesCol;
        private bool _state = false;
        private Dictionary<string, string> _randomcolors;
        
        public ICommand MonthCommand { get; }
        private bool CanExecuteMonthCommand(object obj) => true;
        private void OnExecuteMonthCommand(object obj)
        {
            SelectedMonth = _selectedMonth.AddMonths(Convert.ToInt32(obj));
            ShowChart();
        }
        public ICommand NavigateToListView { get; }
        public bool IsArrowEnable
        {
            get
            {
                if (SelectedMonth.Month == DateTime.Now.Month)
                {
                    return false;
                }
                return true;
            }

        }
        public SeriesCollection SeriesCol
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
                _selectedMonth = value;
                OnPropertyChanged();
                MonthLable = _selectedMonth.ToString("MMM");
            }
        }
        public string MonthLable
        {
            get => _selectedMonth.ToString("MMM");
            set { OnPropertyChanged(nameof(MonthLable)); }
        }

        public string MonthAmount
        {
            get => _monthAmount;
            set
            {
                _monthAmount = value;
                OnPropertyChanged(nameof(MonthAmount));
            }
        }
        private string RandomColor()
        {
            var random = new Random();
            return String.Format("#{0:X6}", random.Next(0x1000000));
           

        }
        private void GetRandomColorByCategory()
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            var sqlData = sql.GetBalanceByMonth(Locator.Data.Id, _selectedMonth, _state);
            foreach (var item in sqlData)
            {
                if (!_randomcolors.ContainsKey(item.Category))
                {
                    _randomcolors.Add(item.Category, RandomColor());
                }
               
            }

        }
        private void ShowChart()
        {
            double sum = 0d;
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            var temp = new SeriesCollection();
            var sqlData = sql.GetBalanceByMonth(Locator.Data.Id, _selectedMonth,_state);
            GetRandomColorByCategory();
            foreach (var item in sqlData)
            {
                var color = sql.GetColors(item.Category, Locator.Data.Id).Select(x => x.Color).FirstOrDefault()??_randomcolors.GetValueOrDefault(item.Category) ;
                temp.Add(new PieSeries
                {
                    
                    Title = item.Category,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(item.Amount)) },
                    Fill = (Brush)new BrushConverter().ConvertFrom(color),
            });;
                sum += double.Parse(item.Amount);
            }
            MonthAmount = sum.ToString();
            SeriesCol = temp;
            OnPropertyChanged(nameof(IsArrowEnable));

        }

        public ChartViewModel(NavigationStore navigationStore,BalanceState balance)
        {
            _randomcolors = new Dictionary<string, string>();
            NavigateToListView = new NavigateCommand<ListViewModel>(navigationStore, () => new ListViewModel(navigationStore, balance,SelectedMonth));
            _state = Locator.Data.State.GetValueOrDefault(balance);
            MonthCommand = new RelayCommand(OnExecuteMonthCommand, CanExecuteMonthCommand);
            SeriesCol = new SeriesCollection();
            ShowChart();
        }
    }
}
