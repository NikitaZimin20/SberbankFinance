using SberbankFinance.Commands;
using SberbankFinance.Model;
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
    class RegisterViewModel:BaseViewModel
    {
       
        public UserModel User { get; private set; }
        public ICommand GoLoginCommand { get; }
        public ICommand NavigateLoginCommand { get; }

        public ICommand RegisterCommand { get; }
        public void OnExecuteRegisterCommand(object p)
        {

            if (User.Password == User.AcceptedPassword)
            {
                AcceptRegistration();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }

        }
        private void AcceptRegistration()
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            bool iscorrect = sql.CheckExistance(User.Name, User.Password).Select(x => x.IsCorrect).FirstOrDefault();
            if (iscorrect)
            {
                MessageBox.Show("Данный пользователь уже существует");
            }
            else
            {                
                sql.Register(User.Name, User.Password);
                MessageBox.Show("Вы успешно зарегистрировались",
               "Attention", MessageBoxButton.OK);
                GoLoginCommand.Execute(this);
            }
            
        }
        public bool CanExecuteRegisterCommand(object p) => true;
        public RegisterViewModel(NavigationStore navigationStore)
        {
            User = new UserModel();
            RegisterCommand = new RelayCommand(OnExecuteRegisterCommand,CanExecuteRegisterCommand );
            GoLoginCommand = new NavigateCommand<LoginViewModel>(navigationStore,()=>new LoginViewModel(navigationStore));
        }
    }
}
