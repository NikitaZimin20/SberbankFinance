using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Stores
{
    internal class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        private BaseViewModel _currentviewmodel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentviewmodel;
            set
            {
                _currentviewmodel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
