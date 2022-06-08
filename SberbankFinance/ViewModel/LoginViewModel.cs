using SberbankFinance.Commands;
using SberbankFinance.FileWorkers;
using SberbankFinance.Model;
using SberbankFinance.Services;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    class LoginViewModel: BaseViewModel
    {
       
        private bool _ischecked = false;
        public bool IsChecked
        {
            get => _ischecked;
            set => _ischecked = value;
        }
        public UserModel User { get;private set; }

        public ICommand NavigateHome { get; private set; }
        public ICommand GoRegisterCommand { get; }

        public ICommand LoginCommand { get; }
        private void OnExecuteLoginCommand(object p)
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            bool iscorrect = sql.CheckExistance(User.Name, User.Password).Select(x => x.IsCorrect).FirstOrDefault();
            if (iscorrect)
             {
                User.Id = sql.ShowId(User.Name).Select(x=>x.Id).FirstOrDefault();
                NavigateHome.Execute(this);
            }
            else MessageBox.Show("Некорекный логин или пароль");
        }

        private bool CanExecuteLoginCommand(object p) => true;

        public LoginViewModel(NavigationStore navigationStore)
        {
            User = new UserModel();
            NavigateHome= new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            LoginCommand =new RelayCommand(OnExecuteLoginCommand, CanExecuteLoginCommand);
            GoRegisterCommand = new NavigateCommand<RegisterViewModel>(navigationStore, () => new RegisterViewModel(navigationStore));
        }

      

    }
}
