using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YNAET.Models
{
    public class ExpenseEntity
    {
        
        public int id { get; }
        public decimal amount { get; set; }
        public string payee { get; set; }
        public string category { get; set; }
        public string account { get; set; }
        public DateTime date { get; set; }
        public Boolean repeat { get; set; }
        public string memo { get; set; }
        public Boolean impulse { get; set; }
        public string colorCode { get; set; }
    }
}
