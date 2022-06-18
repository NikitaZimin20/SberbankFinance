using SberbankFinance.FileWorkers;
using SberbankFinance.Model;
using SberbankFinance.Services;
using SberbankFinance.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Validation
{
    internal class Validator
    {
        private readonly string _field;
        private bool _success = true;
        private Dictionary<Fields, string> _errors;
        public Validator(string field)
        {
            _field = field;
            _errors = new Dictionary<Fields, string>() { {Fields.Name,JsonWorker.GetDescription("IncorectLogin") },
                                                       {Fields.Password,JsonWorker.GetDescription("IncorrectPassword") },
                                                        {Fields.AcceptedPassword,JsonWorker.GetDescription("IncorectAcceptPassword") } };
        }
      

        public Validator MinCharacters(int count)
        {
            if (_field.Length<count)
                _success = false;

            return this;
        }
        public Validator MaxCharacters(int count)
        {
            if (_field.Length > count)
                _success = false;

            return this;
        }
        public Validator IncorectSymbols()
        {
            if (_field.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                _success = false;
            }

            return this;
        }
        public Validator IsPassword()
        {
            //дописать
            return this;
        }
        public Validator IsAccceptPassword(string password)
        {
            if (password!=_field)
            {
                _success=false;
            }
            
            return this;
        }
         
        public void Validate(Fields validationType,ref string exeption)
        {
            
            if (_success == true)
                return;
            exeption=_errors.GetValueOrDefault(validationType);

        }
    }
}
