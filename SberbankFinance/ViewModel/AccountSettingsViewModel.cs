using SberbankFinance.Commands;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SberbankFinance.ViewModel
{
    internal class AccountSettingsViewModel : BaseViewModel
    {
        public ICommand GoSettingsCommand { get; }
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public AccountSettingsViewModel(NavigationStore navigationStore)
        {
            GoSettingsCommand = new NavigateCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore));
        }
    }
}
