using SberbankFinance.Commands;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class SettingsViewModel:BaseViewModel
    {
        public ICommand NavigateHome { get; }
        public ICommand GoAccountSettingsCommand { get; }
        public ICommand DeleteAccount { get; }
        public ICommand NavigateLogin { get; }
        public ICommand NavigateColor { get; }

        private void OnDeleteAccount(object p)
        {
            SqlCrud sqlCrud = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);

            if (MessageBox.Show("Вы уверены",
               "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {


                sqlCrud.DeleteAccount(Locator.Data.Id);
                NavigateLogin.Execute(this);

            }
        }
        private bool CanExecuteDeleteAccount(object p) => true;

        public SettingsViewModel(NavigationStore navigationStore)
        {
            NavigateColor = new NavigateCommand<ChangeColorViewModel>(navigationStore,()=>new ChangeColorViewModel(navigationStore));
            NavigateLogin = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));
            DeleteAccount = new RelayCommand(OnDeleteAccount,CanExecuteDeleteAccount);
            NavigateHome = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            GoAccountSettingsCommand = new NavigateCommand<AccountSettingsViewModel>(navigationStore, () => new AccountSettingsViewModel(navigationStore));
        }
    }
}
