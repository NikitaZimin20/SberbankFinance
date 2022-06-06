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

        public ICommand RegisterComman { get; }
        public void OnExecuteRegisterComman(object p)
        {
           
            if (User.Password==User.AcceptedPassword)
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

            if (MessageBox.Show("Вы успешно зарегистрировалис",
                   "Attention", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
                sql.Register(User.Name, User.Password);
                GoLoginCommand.Execute(this);
            }
        }
        public bool CanExecuteRegisterComman(object p) => true;
        public RegisterViewModel(NavigationStore navigationStore)
        {
            User = new UserModel();
            RegisterComman = new RelayCommand(OnExecuteRegisterComman,CanExecuteRegisterComman );
            GoLoginCommand = new NavigateCommand<LoginViewModel>(navigationStore,()=>new LoginViewModel(navigationStore));
        }
    }
}
