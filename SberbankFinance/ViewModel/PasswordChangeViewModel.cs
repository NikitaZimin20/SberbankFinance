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

        private bool _ischecked = false;
        public bool IsChecked
        {
            get => _ischecked;
            set => _ischecked = value;
        }
        public UserModel User { get; private set; }

        public PasswordChangeViewModel(NavigationStore navigationStore)
        {
            User = new UserModel();
            ChangePasswordCommand = new RelayCommand(OnExecuteChangePasswordCommand, CanExecuteChangePasswordCommand);
        }

        public bool CanExecuteChangePasswordCommand(object p) => true;

        private void OnExecuteChangePasswordCommand(object p)
        {
            //DataContainer data = new DataContainer();
            if (User.Password == User.AcceptedPassword)
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
            bool ispassvalid = sql.CheckPassword(Locator.Data.Id, User.OldPassword).Select(x => x.IsCorrect).FirstOrDefault();
            if (ispassvalid)
            {
                sql.UpdatePassword(User.Password, Locator.Data.Id);
                MessageBox.Show("Вы успешно сменили пароль",
                    "Attention", MessageBoxButton.OK);
                return;
            }
            MessageBox.Show("Старый пароль введён неверно");
        }
    }
}
