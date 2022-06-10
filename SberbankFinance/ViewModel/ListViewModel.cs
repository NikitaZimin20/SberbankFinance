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
       private BalanceModel _selecteduser;

        public ICommand NavigateHome{get;}
        public ObservableCollection<BalanceModel> Balance { get; set; }
        public string Pattern
        {
            get => _pattern;
            set
            {
                _pattern = value;
                SelectedItem = Balance.FirstOrDefault(s => s.StartWith(Pattern));
            }
        }
        public BalanceModel SelectedItem
        {
            get { return _selecteduser; }
            set
            {
                _selecteduser = value;

                OnPropertyChanged(nameof(SelectedItem));

            }
        }
        public ListViewModel(NavigationStore navigation,BalanceState state,DateTime currentmonth)
        {
            ModalNavigationStore modal = new ModalNavigationStore();
            NavigateHome = new NavigateCommand<HomeViewModel>(navigation, () => new HomeViewModel(navigation));
            _sql= new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            Balance = new ObservableCollection<BalanceModel>();
            var sqlData = _sql.GetBalanceByDays(Locator.Data.Id,currentmonth,Locator.Data.State.GetValueOrDefault(state));
            foreach (var item in sqlData)
            {
                Balance.Add(new BalanceModel() { Amount = item.Amount, Type = item.Type, Date = Convert.ToDateTime(item.Date) });
            }
            
        }
    }
}
