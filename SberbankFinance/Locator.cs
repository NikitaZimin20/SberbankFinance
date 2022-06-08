using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance
{
    public static class Locator
    {
        public static DataContainer Data { get; set; }
            = new DataContainer()
            {
                Id=4
            };
    }
}
