using SberbankFinance.Stores;
using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Commands
{
    internal class NavigateModalCommand<TViewModel> : CommandBase
        where TViewModel : BaseViewModel
    {
        private readonly ModalNavigationStore _navigationstore;
        private readonly Func<TViewModel> _createnewmodel;

        public NavigateModalCommand(ModalNavigationStore navigationstore, Func<TViewModel> createnewmodel)
        {
            _navigationstore = navigationstore;
            _createnewmodel = createnewmodel;
        }

        public override void Execute(object parameter)
        {
            _navigationstore.CurrentViewModel = _createnewmodel();
        }
    }
}
