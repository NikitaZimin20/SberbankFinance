using SberbankFinance.Commands;
using SberbankFinance.Services;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    class LoginViewModel:BaseViewModel
    {
     public ICommand GoRegisterCommand { get; }

        string _login;
        string _password;

        //public string Login
        //{
        //    get
        //    {
        //        _login = (_login ?? _user?.Login) ?? string.Empty;
        //        return _login;
        //    }
        //    set
        //    {
        //        _login = value;
        //        ErrorManager.ShowErrors(_login, nameof(Login), Fields.Login);
        //        OnPropertyChanged(nameof(Login));
        //    }
        //}
        //public string Password
        //{
        //    get
        //    {
        //        _password = (_password ?? _user?.Surname) ?? string.Empty;
        //        return _password;
        //    }
        //    set
        //    {
        //        _password = value;
        //        ShowErrors(_password, nameof(Password), Fields.Password);
        //        OnPropertyChanged(nameof(Password));
        //    }
        //}

        public LoginViewModel(NavigationStore navigationStore)
        {
            GoRegisterCommand = new NavigateCommand<RegisterViewModel>(navigationStore, () => new RegisterViewModel(navigationStore));
        }
    }
}
