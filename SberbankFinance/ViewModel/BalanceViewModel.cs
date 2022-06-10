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
        SqlCrud _sql;
        public ICommand NavigateToHomeView { get; }
        public ICommand NoteCommand { get; }
        private readonly BalanceState _balanceState;
     
        public string[] Categories
        {
            get
            {
                if (_balanceState==BalanceState.Outcome)
                {
                    return _sql.GetCategory(false).Select(x => x.Categories).Append("Добавить новую категорию").ToArray();
                }
                return _sql.GetCategory(true).Select(x => x.Categories).Append("Добавить новую категорию").ToArray();
            }
        }
      
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
            _sql.AddOutcome(BalanceModel,Locator.Data.Id);
            NavigateToHomeView.Execute(this);
        }
        private bool CanExecuteNoteCommand(object obj) => true;


        public BalanceViewModel(NavigationStore navigationStore,BalanceState state)
        {
            
            _sql= new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
           
            BalanceModel = new BalanceModel(navigationStore,state);
            _balanceState=state;
            NavigateToHomeView= new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            NoteCommand = new RelayCommand(OnExecuteNoteCommand,CanExecuteNoteCommand);
        }
    }
}
