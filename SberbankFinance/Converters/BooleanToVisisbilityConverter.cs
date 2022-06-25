using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SberbankFinance.Converters
{
    internal class BooleanToVisisbilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisisbilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}
