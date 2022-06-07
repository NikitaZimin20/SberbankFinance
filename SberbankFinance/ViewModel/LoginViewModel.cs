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
    class LoginViewModel: BaseViewModel,INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        private readonly ErrorViewModel _errorsViewModel;

        private readonly Dictionary<Fields, ErrorsModel> _errorlist;

        private bool _ischecked = false;
        public bool IsChecked
        {
            get => _ischecked;
            set => _ischecked = value;
        }

        string _name;
        string _password;
        string _acceptedPassword;

        public bool HasErrors => _errorsViewModel.HasErrors;

        public UserModel User { get;private set; }

        public ICommand NavigateHome { get; private set; }
        public ICommand GoRegisterCommand { get; }

        public ICommand LoginCommand { get; }
        private void OnExecuteLoginCommand(object p)
        {
            SqlCrud sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
            bool iscorrect = sql.CheckExistance(Name, Password).Select(x => x.IsCorrect).FirstOrDefault();
            if (iscorrect)
             {
                User.Id = sql.ShowId(User.Name).Select(x=>x.Id).FirstOrDefault();
                NavigateHome.Execute(this);
            }
            else MessageBox.Show("Некорекный логин или пароль");
        }

        public string Name
        {
            get
            {
                _name = (_name ?? User?.Name) ?? string.Empty;
                return _name;
            }
            set
            {
                _name = value;
                ShowErrors(_name, nameof(Name), Fields.Name);
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Password
        {
            get
            {
                _password = (_password ?? User?.Password) ?? string.Empty;
                return _password;
            }
            set
            {
                _password = value;
                ShowErrors(_password, nameof(Password), Fields.Password);
                OnPropertyChanged(nameof(Password));
            }
        }
        public string AcceptedPassword
        {
            get
            {
                _acceptedPassword = (_acceptedPassword ?? User?.AcceptedPassword) ?? string.Empty;
                return _acceptedPassword;
            }
            set
            {
                _acceptedPassword = value;
                ShowErrors(_acceptedPassword, nameof(AcceptedPassword), Fields.AcceptedPassword);
                OnPropertyChanged(nameof(AcceptedPassword));
            }
        }

        private bool CanExecuteLoginCommand(object p) => true;

        private void ShowErrors(string property, string propetyName, Fields type)
        {
            _errorsViewModel.ClearErrors(nameof(property));
            if (property.Length < 2)

                _errorsViewModel.AddError(propetyName, _errorlist[type].MoreSymbols);

            else if (property.Length > 50)

                _errorsViewModel.AddError(propetyName, _errorlist[type].LessSymbols);

            else if (property.Any(ch => !Char.IsLetterOrDigit(ch)))

                _errorsViewModel.AddError(propetyName, _errorlist[type].LessSymbols);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        public LoginViewModel(NavigationStore navigationStore)
        {
            _errorlist = new Dictionary<Fields, ErrorsModel>() { {Fields.Name, new ErrorsModel { MoreSymbols = JsonWorker.GetDescription("MoreSymbols", "Name"), LessSymbols = JsonWorker.GetDescription("LessSymbols", "Name"), Incorrectsymbols = JsonWorker.GetDescription("Incorrect Symbols") } },
                { Fields.Password,new ErrorsModel { MoreSymbols = JsonWorker.GetDescription("MoreSymbols", "Password"), LessSymbols = JsonWorker.GetDescription("LessSymbols", "Password"), Incorrectsymbols = JsonWorker.GetDescription("Incorrect Symbols") } },
                {Fields.AcceptedPassword,new ErrorsModel { MoreSymbols = JsonWorker.GetDescription("MoreSymbols", "AcceptedPassword"), LessSymbols = JsonWorker.GetDescription("LessSymbols", "AcceptedPassword"), Incorrectsymbols = JsonWorker.GetDescription("Incorrect Symbols") } } };
            _errorsViewModel = new ErrorViewModel();
            _errorsViewModel.ErrorsChanged += Changed;
            User = new UserModel();
            NavigateHome= new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            LoginCommand =new RelayCommand(OnExecuteLoginCommand, CanExecuteLoginCommand);
            GoRegisterCommand = new NavigateCommand<RegisterViewModel>(navigationStore, () => new RegisterViewModel(navigationStore));
        }

        private void Changed(object? sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

    }
}
