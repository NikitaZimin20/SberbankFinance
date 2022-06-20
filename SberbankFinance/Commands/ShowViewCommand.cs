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
        private HomeViewModel _viewModel;
        public ShowViewCommand(HomeViewModel viewModel,NavigationStore navigationStore)
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
      
        }
    }
}
