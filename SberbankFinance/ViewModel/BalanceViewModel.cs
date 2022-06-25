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
        string _selecteditem;
        public BalanceModel BalanceModel { get; }
        SqlCrud _sql;
        private ICommand NavigateToNewCategory { get; }
        public ICommand NavigateToHomeView { get; }
        public ICommand NoteCommand { get; }
        private readonly BalanceState _balanceState;
     
        public string[] Categories
        {
            get
            {
                if (_balanceState==BalanceState.Outcome)
                {
                    return _sql.GetCategory().Where(x=>x.Type==false).Select(x=>x.Category).Append("Добавить новую категорию").ToArray();
                }
                return _sql.GetCategory().Where(x => x.Type == true).Select(x => x.Category).Append("Добавить новую категорию").ToArray();
            }
          
        }
        public string SelectedItem
        {
            get => _selecteditem;
            set
            {
                if (value== "Добавить новую категорию")
                {
                   NavigateToNewCategory.Execute(this);
                }
                _selecteditem = value;
                OnPropertyChanged(nameof(SelectedItem));
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
            
            NavigateToNewCategory = new NavigateCommand<NewCategoryViewModel>(navigationStore,()=>new NewCategoryViewModel(navigationStore,state));
            _sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            BalanceModel = new BalanceModel();
            _balanceState=state;
            NavigateToHomeView= new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            NoteCommand = new RelayCommand(OnExecuteNoteCommand,CanExecuteNoteCommand);
        }
    }
}
