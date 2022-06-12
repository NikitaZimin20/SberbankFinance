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
    internal class ShowViewCommand : ICommand
    {
        private readonly NavigationStore _navigationStore;  
        private BaseViewModel viewModel;
        public ShowViewCommand(BaseViewModel viewModel, NavigationStore navigationStore)
        {
            _navigationStore= navigationStore;
            this.viewModel = viewModel;
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
                viewModel.SelectedViewModel = new IncomeViewModel(_navigationStore);
            }
            else if (parameter.ToString() == "Outcome")
            {
                viewModel.SelectedViewModel = new OutcomeViewModel(_navigationStore);
            }
            else if (parameter.ToString() == "PasswordChange")
            {
                viewModel.SelectedViewModel = new PasswordChangeViewModel(_navigationStore);
            }
            else if (parameter.ToString() == "LoginChange")
            {
                viewModel.SelectedViewModel = new LoginChangeViewModel(_navigationStore);
            }
        }
    }
}
