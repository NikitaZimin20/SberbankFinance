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
        private readonly ModalNavigationStore _modalnavigationstore;
        public BaseViewModel CurrentViewModel => _navigationstore.CurrentViewModel;
        public BaseViewModel CurrentModalViewModel=>_modalnavigationstore.CurrentViewModel;
        public bool IsModalOpen => _modalnavigationstore.IsOpen;
        public MainViewModel(NavigationStore navigationstore, ModalNavigationStore modalnavigationstore)
        {

            _navigationstore = navigationstore;
            _navigationstore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalnavigationstore = modalnavigationstore;
            _modalnavigationstore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
