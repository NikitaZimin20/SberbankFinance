using SberbankFinance.Commands;
using SberbankFinance.Model;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.States;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class ListViewModel : BaseViewModel
    {  private SqlCrud _sql;
       private string _pattern;
       private List<BalanceModel>  _balancestore;
       private bool _state=false;

        public ICommand NavigateHome{get;}
        public ObservableCollection<BalanceModel> Balance { get; set; }
        public string Label
        {
            get
            {
                string month = Balance.Select(x => x.Date.ToString("MMM")).FirstOrDefault();

                if (!_state)
                {
                    return $"Расходы за {month}";
                }
                return $"Доходы за {month}";
            }
        }
        public string Pattern
        {
            get => _pattern;
            set
            {
                
                _pattern = value;
                FindbyPattern(value);
                OnPropertyChanged(nameof(Pattern));

            }
        }

        public List<BalanceModel>  BalanceStore
        {
            get { return _balancestore; }
            set
            {
                _balancestore = value;
                
                OnPropertyChanged(nameof(BalanceStore));

            }
        }
        private void FindbyPattern(string pattern)
        {
           
            if (pattern != String.Empty)
            {
                Balance.Clear();
                string patternreplace = pattern.Substring(0, 1).ToUpper() + pattern.Substring(1);
                foreach (var item in BalanceStore)
                {

                    if (item.Category.StartsWith(patternreplace)||item.Amount.StartsWith(pattern)||item.StringDate.StartsWith(pattern))
                    {
                        Balance.Add(item);
                    }

                }
            }
            else AddBalanceList(BalanceStore);
           
            
        }
        private void AddBalanceList(List<BalanceModel> source)
        {
            Balance.Clear();
            foreach (var item in source)
            {
                Balance.Add(new BalanceModel() { Amount = item.Amount, Category = item.Category,Date=item.Date ,StringDate = item.Date.ToString("D")});
            }
        }
        public ListViewModel(NavigationStore navigation,BalanceState state,DateTime currentmonth)
        {
            ModalNavigationStore modal = new ModalNavigationStore();
            NavigateHome = new NavigateCommand<HomeViewModel>(navigation, () => new HomeViewModel(navigation));
            _sql= new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            Balance = new ObservableCollection<BalanceModel>();
            _state = Locator.Data.State.GetValueOrDefault(state);
            var sqlData = _sql.GetBalanceByDays(Locator.Data.Id,currentmonth,_state);
            AddBalanceList(sqlData);
            BalanceStore = Balance.ToList() ;
            
        }
    }
}
