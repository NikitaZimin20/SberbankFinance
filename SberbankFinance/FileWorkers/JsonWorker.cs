using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SberbankFinance.FileWorkers
{
    internal class JsonWorker
    {
        public static string GetDescription(string message)
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(System.Configuration.ConfigurationManager.ConnectionStrings["JsonPath"].ConnectionString);
            var config = builder.Build();

            return config[message];
        }






    }
}
