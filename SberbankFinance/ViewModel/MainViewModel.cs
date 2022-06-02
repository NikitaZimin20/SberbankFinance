using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.ViewModel
{
    internal class MainViewModel:BaseViewModel
    {
        private readonly NavigationStore _navigationstore;
        public BaseViewModel CurrentViewModel => _navigationstore.CurrentViewModel;
        public MainViewModel(NavigationStore navigationstore)
        {

            _navigationstore = navigationstore;
            _navigationstore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
