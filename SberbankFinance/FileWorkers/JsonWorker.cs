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
        public static string GetDescription(string message, string replacement = "")
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(System.Configuration.ConfigurationManager.ConnectionStrings["JsonPath"].ConnectionString);
            var config = builder.Build();
            if (!replacement.Equals(string.Empty))
                return config[message].Replace("{field}", replacement);



            return config[message];
        }






    }
}
