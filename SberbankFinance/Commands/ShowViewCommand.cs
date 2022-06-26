using SberbankFinance.Stores;
using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.Commands
{
    internal class ShowViewCommand<T> : ICommand where T : VariableViewModel
    {
        private readonly NavigationStore _navigationStore;  
        private readonly T _viewModel;
        public ShowViewCommand(T viewModel,NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Income")
            {
                _viewModel.SelectedViewModel = new IncomeViewModel(_navigationStore);
            }
            else if (parameter.ToString() == "Outcome")
            {
                _viewModel.SelectedViewModel = new OutcomeViewModel(_navigationStore);
            }

            else if (parameter.ToString() == "LoginChange")
            {
                _viewModel.SelectedViewModel = new LoginChangeViewModel(_navigationStore);
            }
            else if (parameter.ToString() == "PasswordChange")
            {
                _viewModel.SelectedViewModel = new PasswordChangeViewModel(_navigationStore);
            }    
      
        }
    }
}
