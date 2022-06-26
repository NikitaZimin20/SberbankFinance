using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
using System.Windows;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class LoginChangeViewModel : BaseViewModel
    {
        public ICommand ChangeLoginCommand { get; }
        public UserModel User { get; private set; }

        public LoginChangeViewModel(NavigationStore navigationStore)
        {
            User = new UserModel();
            ChangeLoginCommand = new RelayCommand(OnExecuteChangeLoginCommand, CanExecuteChangeLoginCommand);
        }

        private void OnExecuteChangeLoginCommand(object p)
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            sql.UpdateLogin(User.Name, Locator.Data.Id);
            MessageBox.Show("Вы успешно сменили логин",
                "Attention", MessageBoxButton.OK);
        }

        private bool CanExecuteChangeLoginCommand(object p) => true;
    }
}
