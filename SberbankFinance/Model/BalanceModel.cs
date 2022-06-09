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
        private ICommand NavigateNewCategory { get; }
        private readonly BalanceState _state;
        private string _amount;
        private string _type;
        private DateTime _data=DateTime.Now;

        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public string Type
        {
            get => _type;
            set
            {
                if (value== "Добавить новую категорию")
                {
                    NavigateNewCategory.Execute(this);
                }
                _type = value;
                OnPropertyChanged(nameof(Type));
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
        public BalanceModel(NavigationStore store,BalanceState state)
        {
           
            _state = state;
            NavigateNewCategory = new NavigateCommand<NewCategoryViewModel>(store, () => new NewCategoryViewModel(store, _state));
           
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
