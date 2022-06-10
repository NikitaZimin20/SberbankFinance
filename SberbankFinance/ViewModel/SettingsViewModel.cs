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
    internal class SettingsViewModel:BaseViewModel
    {
        public ICommand NavigateHome { get; }
        public ICommand GoAccountSettingsCommand { get; }

        public SettingsViewModel(NavigationStore navigationStore)
        {
            NavigateHome = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            GoAccountSettingsCommand = new NavigateCommand<AccountSettingsViewModel>(navigationStore, () => new AccountSettingsViewModel(navigationStore));
        }
    }
}
