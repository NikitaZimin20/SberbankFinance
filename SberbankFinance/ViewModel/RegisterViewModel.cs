using SberbankFinance.Commands;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    class RegisterViewModel:BaseViewModel
    {
        public ICommand GoLoginCommand { get; }


        public RegisterViewModel(NavigationStore navigationStore)
        {
            GoLoginCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
        }
    }
}
