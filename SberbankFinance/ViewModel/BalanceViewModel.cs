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
    internal class BalanceViewModel:BaseViewModel
    {
        public BalanceModel BalanceModel { get; }
        public ICommand NavigateToHomeView { get; }
        public ICommand NoteCommand { get; }
        private readonly BalanceState _balanceState;
       
        public string Label
        {
            get
            {
                if (_balanceState==BalanceState.Outcome)
                {
                    return "Расходов";
                }
                return "Доходов";
            }
        }
        private void OnExecuteNoteCommand(object p)
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);

        }
        private bool CanExecuteNoteCommand(object obj) => true;


        public BalanceViewModel(NavigationStore navigationStore,BalanceState state)
        {
            BalanceModel = new BalanceModel();
            _balanceState=state;
            NavigateToHomeView= new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            NoteCommand = new RelayCommand(OnExecuteNoteCommand,CanExecuteNoteCommand);
        }
    }
}
