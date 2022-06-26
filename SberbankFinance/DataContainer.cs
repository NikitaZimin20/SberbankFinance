using SberbankFinance.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SberbankFinance
{
    public class DataContainer : OnPropertyChangedClass
    {
        private Dictionary<int, Dictionary<string, Brushes>> _usercolors;
        private Dictionary<BalanceState, bool> _state;
        private int _id;
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        internal Dictionary<BalanceState,bool> State { get => _state; set => SetProperty(ref _state, value); }
        internal Dictionary<int,Dictionary<string,Brushes>> UsersColors { get => _usercolors; set => SetProperty(ref _usercolors, value); }
       
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
