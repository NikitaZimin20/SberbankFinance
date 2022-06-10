using SberbankFinance.Stores;
using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Services
{
    internal class ModalNavigationService<TViewModel> : INavigationService
         where TViewModel : BaseViewModel
    {
        private readonly ModalNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        internal ModalNavigationService(ModalNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}

