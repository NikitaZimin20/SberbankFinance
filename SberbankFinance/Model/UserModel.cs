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
using SberbankFinance.Validation;

namespace SberbankFinance.Model
{
    internal class UserModel:INotifyPropertyChanged,INotifyDataErrorInfo
    {
        private int _id;
        private string _name;
        private string _password;
        private string _acceptedpassword;
        string _exeption = string.Empty;
        private readonly ErrorViewModel _errorsViewModel;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
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
              
                Name.Rules().MinCharacters(2).MaxCharacters(50).Validate(Fields.Name, out _exeption);
                ShowErrors(nameof(Name));  
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Password.Rules().MinCharacters(5).MaxCharacters(50).Validate(Fields.Password,out _exeption);
                ShowErrors(nameof(Password));
                OnPropertyChanged(nameof(Password));
            }
        }
        public string AcceptedPassword
        {
            get => _acceptedpassword;
            set
            {
                _acceptedpassword = value;
                AcceptedPassword.Rules().IsAccceptPassword(Password).Validate(Fields.AcceptedPassword, out _exeption);
                ShowErrors(nameof(Password));
                OnPropertyChanged(nameof(AcceptedPassword));
            }
        }
        public bool HasErrors => _errorsViewModel.HasErrors;
        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }
        private void Changed(object? sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }
        private void ShowErrors(string propertyName)
        {
            if (!String.IsNullOrEmpty(_exeption))
            {
                _errorsViewModel.ClearErrors(propertyName);
                _errorsViewModel.AddError(propertyName, _exeption);
            }
            else
            {
                _errorsViewModel.ClearErrors(propertyName);
            }         
        }
            

        public UserModel()
        {
            _errorsViewModel = new ErrorViewModel();
            _errorsViewModel.ErrorsChanged += Changed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
