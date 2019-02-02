using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YNAET.Entities
{
    public class Expense
    {
        
        public virtual long? id { get; set; }
        public virtual decimal? amount { get; set; }
        public virtual string payee { get; set; }
        public virtual string category { get; set; }
        public virtual string account { get; set; }
        public virtual DateTime? date { get; set; }
        public virtual Boolean? repeat { get; set; }
        public virtual string memo { get; set; }
        public virtual Boolean? impulse { get; set; }
        public virtual string colorCode { get; set; }
    }
}
