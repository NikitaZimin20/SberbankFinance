using Microsoft.Win32;
using SberbankFinance.Commands;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SberbankFinance.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        
        public ICommand NavigateHome { get; }
        public ICommand GoAccountSettingsCommand { get; }

        public ICommand SetImageCommand { get; }
        private ImageSource _image;
        public SettingsViewModel(NavigationStore navigationStore)
        {
            NavigateHome = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            GoAccountSettingsCommand = new NavigateCommand<AccountSettingsViewModel>(navigationStore, () => new AccountSettingsViewModel(navigationStore));
            SetImageCommand = new RelayCommand(OnExecuteSetImageCommand,CanExecuteSetImageCommand);
            LoadImage();
          
        }


        public void LoadImage()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Images", Locator.Data.Id.ToString() + ".jpg");
            if (File.Exists(path))
            {
                ImageSource = BitmapFromUri(new Uri(path));
                HaveImage = true;
            }
        }

        public bool CanExecuteSetImageCommand(object p) => true;

        public async  void OnExecuteSetImageCommand(object p)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg;)|*.jpg;";
            if (fd.ShowDialog() == true)
            {
                var fileNameToSave = Locator.Data.Id.ToString() + Path.GetExtension(fd.FileName);
                var imagePath = Path.Combine(Environment.CurrentDirectory, "Images", fileNameToSave);
                 
                File.Copy(fd.FileName, imagePath, true);
                LoadImage();
            }
                
            
        }
        private async Task Task(string name,string path,bool state=true )
        {

        }

        public ImageSource ImageSource
        {
            get=> _image;
            set
            {   
                _image = value;
                OnPropertyChanged();
            }
        }
        public bool HaveImage { get; private set; }

        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CreateOptions= BitmapCreateOptions.IgnoreImageCache;
            bitmap.EndInit();
            return bitmap;
        }
    }
}
