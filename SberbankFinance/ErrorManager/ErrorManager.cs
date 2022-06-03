using SberbankFinance.Model;
using SberbankFinance.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.ErrorManager
{
    abstract class ErrorManager : INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        private readonly Dictionary<string, List<string>> _propertyyErrors = new Dictionary<string, List<string>>() { };

        private readonly Dictionary<Fields, ErrorsModel> _errorlist;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyyErrors.GetValueOrDefault(propertyName, null);
        }
        public void ClearErrors(string propertyname)
        {
            if (_propertyyErrors.Remove(propertyname))

                OnErrorsChanged(propertyname);
        }
        public bool HasErrors => _propertyyErrors.Any();
        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyyErrors.ContainsKey(propertyName))
            {
                _propertyyErrors.Add(propertyName, new List<string>());
            }
            _propertyyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string? propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        }

        public virtual void ShowErrors(string property, string propetyName, Fields type)
        {
            ClearErrors(nameof(property));
            if (property.Length < 2)

                AddError(propetyName, _errorlist[type].MoreSymbols);

            else if (property.Length > 50)

                AddError(propetyName, _errorlist[type].LessSymbols);

            else if (property.Any(ch => !Char.IsLetterOrDigit(ch)))

                AddError(propetyName, _errorlist[type].LessSymbols);
            
        }


    }
}
