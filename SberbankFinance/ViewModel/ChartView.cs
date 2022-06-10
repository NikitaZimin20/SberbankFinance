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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class ChartView:BaseViewModel
    {
        string _monthAmount;
        DateTime _selectedMonth = DateTime.Now;
        private SeriesCollection _seriesCol;
        private bool _state = false;
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

        private void ShowChart()
        {
            double sum = 0d;
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            var temp = new SeriesCollection();
            var sqlData = sql.GetBalanceByMonth(Locator.Data.Id, _selectedMonth,_state);
            foreach (var item in sqlData)
            {
                temp.Add(new PieSeries { Title = item.Type, Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(item.Amount)) } });
                sum += double.Parse(item.Amount);
            }
            MonthAmount = sum.ToString();
            SeriesCol = temp;
            OnPropertyChanged(nameof(IsArrowEnable));

        }

        public ChartView(NavigationStore navigationStore,BalanceState balance)
        {
            NavigateToListView = new NavigateCommand<ListViewModel>(navigationStore, () => new ListViewModel(navigationStore, BalanceState.Outcome,SelectedMonth));
            _state = Locator.Data.State.GetValueOrDefault(balance);
            MonthCommand = new RelayCommand(OnExecuteMonthCommand, CanExecuteMonthCommand);
            SeriesCol = new SeriesCollection();
            ShowChart();
        }
    }
}
