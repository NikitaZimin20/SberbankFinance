using SberbankFinance.Commands;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.States;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class HomeViewModel:BaseViewModel
    {

        public ICommand NavigateToIncomeCommand { get; }
        public ICommand NavigateToOutcomeCommand { get; }
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateToOutcomeCommand= new NavigateCommand<BalanceViewModel>(navigationStore, () => new BalanceViewModel(navigationStore,BalanceState.Outcome));
            NavigateToIncomeCommand = new NavigateCommand<BalanceViewModel>(navigationStore, () => new BalanceViewModel(navigationStore,BalanceState.Income));
            SelectedViewModel = new OutcomeViewModel();
            UpdateViewCommand = new ShowViewCommand(this);
            
         
        }
    }
}
