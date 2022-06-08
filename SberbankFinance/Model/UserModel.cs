using SberbankFinance.FileWorkers;
using SberbankFinance.Services;
using SberbankFinance.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Model
{
    internal class UserModel:INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private int _id;
        private string _name;
        private string _password;
        private string _acceptedpassword;
        private readonly ErrorViewModel _errorsViewModel;
        public  event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;

        private readonly Dictionary<Fields, ErrorsModel> _errorlist;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name { get => _name;
            set
            {
                _name = value;
                ShowErrors(_name,nameof(Name),Fields.Name);
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                ShowErrors(_password, nameof(Password), Fields.Password);
                OnPropertyChanged(nameof(Password));
            }
        }
        public string AcceptedPassword
        {
            get => _acceptedpassword;
            set
            {
                _acceptedpassword = value;
                ShowErrors(_acceptedpassword, nameof(AcceptedPassword), Fields.AcceptedPassword);
                OnPropertyChanged(nameof(AcceptedPassword));
            }
        }

        private void ShowErrors(string property, string propetyName, Fields type)
        {
            _errorsViewModel.ClearErrors(propetyName);
            if (property.Length < 2 && type==Fields.Name)

                _errorsViewModel.AddError(propetyName, _errorlist[type].MoreSymbols);
            else if (property.Length > 50 && type==Fields.Name)
            {
                _errorsViewModel.AddError(propetyName, _errorlist[type].LessSymbols);
            }

            else if (property.Length < 6 &&type==Fields.Password)

                _errorsViewModel.AddError(propetyName, _errorlist[type].IncorrectPassword);

            else if (property.Any(ch => !Char.IsLetterOrDigit(ch))&&type==Fields.Name)

                _errorsViewModel.AddError(propetyName, _errorlist[type].IncorrectSymbols);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }
        public UserModel()
        {
            _errorsViewModel = new ErrorViewModel();
            _errorlist = new Dictionary<Fields, ErrorsModel>() { {Fields.Name, new ErrorsModel { MoreSymbols = JsonWorker.GetDescription("MoreSymbols"), LessSymbols = JsonWorker.GetDescription("LessSymbols"), IncorrectSymbols = JsonWorker.GetDescription("Incorrect Symbols") } },
                { Fields.Password,new ErrorsModel {IncorrectPassword=JsonWorker.GetDescription("IncorrectPassword")  } },
                {Fields.AcceptedPassword,new ErrorsModel { MoreSymbols = JsonWorker.GetDescription("MoreSymbols"), LessSymbols = JsonWorker.GetDescription("LessSymbols"), IncorrectSymbols = JsonWorker.GetDescription("Incorrect Symbols") } } };
            _errorsViewModel.ErrorsChanged += Changed;
        }

     

        private void Changed(object? sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
