using SberbankFinance.Commands;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SberbankFinance.ViewModel
{
    internal class ChangeColorViewModel:BaseViewModel
    {
        private ICommand NavigateSettings { get; }
        public ICommand AddColorCommand { get; }

        private void OnAddColorCommand(object p)
        {
            _sql.AddColor(Locator.Data.Id,SelectedCategory,SelectedColor.Split(new string[] { "System.Windows.Media.Color " }, StringSplitOptions.None)[1]);
            NavigateSettings.Execute(this);
        }
        private bool CanExecuteAddColorCommand(object p) => true;
        private readonly SqlCrud _sql;
       public bool IsEnable { get; set; }
        public string[] Categories
        {
            get => _sql.GetCategory().Select(x => x.Category).ToArray();
        }

        public string SelectedColor { get;
            set; }
        public string SelectedCategory { get; set; }    
        
           
        
        public ChangeColorViewModel(NavigationStore navigationStore)
        {
            NavigateSettings = new NavigateCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(navigationStore));
            AddColorCommand = new RelayCommand(OnAddColorCommand, CanExecuteAddColorCommand);
            _sql = new SqlCrud(ConfigurationManager.ConnectionStrings["any"].ConnectionString);
        }

    }
}
