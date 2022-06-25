using SberbankFinance.States;
using SberbankFinance.Stores;
using SberbankFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SberbankFinance
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            ModalNavigationStore modalNavigationStore = new ModalNavigationStore();

            navigationStore.CurrentViewModel = new ChangeColorViewModel(navigationStore);
         

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore,modalNavigationStore),
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
