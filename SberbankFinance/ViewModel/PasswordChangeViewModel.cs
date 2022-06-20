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
    internal class PasswordChangeViewModel : BaseViewModel
    {
        public ICommand ChangePasswordCommand { get; }

        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string AcceptedPassword { get; set; }

        public PasswordChangeViewModel(NavigationStore navigationStore)
        {
            ChangePasswordCommand = new RelayCommand(OnExecuteChangePasswordCommand, CanExecuteChangePasswordCommand);
        }

        public bool CanExecuteChangePasswordCommand(object p) => true;

        private void OnExecuteChangePasswordCommand(object p)
        {
            //DataContainer data = new DataContainer();
            if (Password == AcceptedPassword && CheckPassword())
            {
                AcceptChangePassword();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }

        }

        private void AcceptChangePassword()
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            sql.UpdatePassword(Password, Locator.Data.Id);
            MessageBox.Show("Вы успешно сменили пароль",
                "Attention", MessageBoxButton.OK);
        }

        private bool CheckPassword()
        {
            return true;
        }
    }
}
