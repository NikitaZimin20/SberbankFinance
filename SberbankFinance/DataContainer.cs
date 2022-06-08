using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance
{
    public class DataContainer : OnPropertyChangedClass
    {
     
        private int _id;
        public int Id { get => _id; set => SetProperty(ref _id, value); }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
