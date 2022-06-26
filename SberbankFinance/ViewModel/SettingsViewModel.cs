using Microsoft.Win32;
using SberbankFinance.Commands;
using SberbankFinance.SqlDataAccess;
using SberbankFinance.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;

namespace SberbankFinance.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        public ICommand NavigateHome { get; }
        public ICommand GoAccountSettingsCommand { get; }
        public ICommand SetImageCommand { get; }
        ImageSource _ImageSource;

        public SettingsViewModel(NavigationStore navigationStore)
        {
            LoadImage(Path.Combine(Environment.CurrentDirectory, "Images", Locator.Data.Id.ToString() + ".jpg"));
            SetImageCommand = new RelayCommand(OnExecuteSetImageCommand, CanExecuteSetImageCommand);
            NavigateHome = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            GoAccountSettingsCommand = new NavigateCommand<AccountSettingsViewModel>(navigationStore, () => new AccountSettingsViewModel(navigationStore));
        }

        public void LoadImage(string path)
        {
            if (File.Exists(path))
            {
                ImageSource = BitmapFromUri(path);
                HaveImage = true;
            }
        }

        public bool CanExecuteSetImageCommand(object p) => true;

        public void OnExecuteSetImageCommand(object p)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg;)|*.jpg;";
            if (fd.ShowDialog() == true)
            {
                var fileNameToSave = Locator.Data.Id.ToString() + Path.GetExtension(fd.FileName);
                var imagePath = Path.Combine(Environment.CurrentDirectory, "Images", fileNameToSave);
                File.Copy(fd.FileName, imagePath, true);
                LoadImage(fd.InitialDirectory + fd.FileName);
                GC.Collect();
            }
        }

        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
        public bool HaveImage = false;

        public static ImageSource BitmapFromUri(string filepath)
        {
            var bitmap = new BitmapImage();
            using (var fs = new FileStream(filepath, FileMode.Open))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = fs;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            bitmap.Freeze();
            return bitmap;
        }
    }
}
