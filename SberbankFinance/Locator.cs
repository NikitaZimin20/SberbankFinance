using SberbankFinance.States;
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
                Id=4,
                State=new Dictionary<BalanceState, bool>() { { BalanceState.Income,true },{ BalanceState.Outcome,false} },
                UsersColors=new Dictionary<int, Dictionary<string, System.Windows.Media.Brushes>>()
                {

                }
            };

    }
}
