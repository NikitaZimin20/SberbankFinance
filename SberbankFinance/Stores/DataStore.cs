using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Stores
{
    internal static class DataStore
    {
        public delegate int MyEvent(string data);
        public static MyEvent EventHandler;
    }
}
