using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;


namespace SberbankFinance
{
    internal class Configuration
    {
        

         public static string Get()
        {
            string appFolderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string resourcesFolderPath = Path.Combine(
            Directory.GetParent(appFolderPath).Parent.FullName, @"Resources\Sberbank.db");
            return resourcesFolderPath;

            
        }



    }
}
