using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SberbankFinance.Model
{
    internal class SqlDataModel
    {
        public string Wastes { get; set; }
        public string Username { get; set; }
        public bool IsCorrect { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public int Id { get; set; }
      
        public string Amount { get; set; }
        public string Date { get; set; }
    }
}
