using SberbankFinance.Commands;
using SberbankFinance.States;
using SberbankFinance.Stores;
using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.Model
{
    internal class BalanceModel:INotifyPropertyChanged
    {
      
        private string _amount;
        private string _category;
        private DateTime _data;
        private string _stribgdate;

        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public string Category
        {
            get => _category;
            set
            {
                
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public DateTime Date
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public  string StringDate
        {
            get => _stribgdate;
            set
            {
                _stribgdate = value;
                OnPropertyChanged(nameof(StringDate));
            }
        }
        public bool Type { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
