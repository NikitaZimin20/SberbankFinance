using SberbankFinance.Commands;
using SberbankFinance.Stores;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class AccountSettingsViewModel : VariableViewModel
    {
        public ICommand GoSettingsCommand { get; }
        public ICommand NavigateToPasswordChangeCommand { get; }
        public ICommand NavigateToLoginChangeCommand { get; }
        public ICommand UpdateViewCommand { get; set; }

        public AccountSettingsViewModel(NavigationStore navigationStore)
        {
            GoSettingsCommand = new NavigateCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore));
            NavigateToPasswordChangeCommand = new NavigateCommand<PasswordChangeViewModel>(navigationStore, () => new PasswordChangeViewModel(navigationStore));
            NavigateToLoginChangeCommand = new NavigateCommand<LoginChangeViewModel>(navigationStore, () => new LoginChangeViewModel(navigationStore));
            //UpdateViewCommand = new ShowViewCommand(this,navigationStore);
            SelectedViewModel = new LoginChangeViewModel(navigationStore);
        }
    }
}
