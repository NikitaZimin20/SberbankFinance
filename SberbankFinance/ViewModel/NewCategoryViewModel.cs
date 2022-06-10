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
    internal class NewCategoryViewModel:BaseViewModel
    {
        private readonly bool _state=false;
        public string InputText { get;  set; }
        public ICommand BackCommand { get; }
        ICommand NavigateToBalance { get; }

        private void OnExecuteBackCommand(object param)
        {
            if (Convert.ToBoolean(param) is true)
            {
                SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
                sql.AddCategory(InputText, _state);
            }
            NavigateToBalance.Execute(this);
        }
        private bool CanExecuteBackCommand(object obj) => true;

        public NewCategoryViewModel(NavigationStore navigationStore,BalanceState state)
        {
            if (state==BalanceState.Income)
            {
                _state=true;
            }
            BackCommand = new RelayCommand(OnExecuteBackCommand,CanExecuteBackCommand);
            NavigateToBalance = new NavigateCommand<BalanceViewModel>(navigationStore, () => new BalanceViewModel(navigationStore, state));

        }
    }
}
