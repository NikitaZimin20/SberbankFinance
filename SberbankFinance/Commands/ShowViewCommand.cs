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
        private HomeViewModel viewModel;
        public ShowViewCommand(HomeViewModel viewModel)
        {
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
                viewModel.SelectedViewModel = new IncomeViewModel();
            }
            else if (parameter.ToString() == "Outcome")
            {
                viewModel.SelectedViewModel = new OutcomeViewModel();
            }
        }
    }
}
