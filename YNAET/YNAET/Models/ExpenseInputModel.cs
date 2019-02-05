using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YNAET.Models
{
    public class ExpenseInputModel
    {
        public virtual long Id { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string Payee { get; set; }
        public virtual string Category { get; set; }
        public virtual string Account { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Boolean Repeat { get; set; }
        public virtual string Memo { get; set; }
        public virtual Boolean Impulse { get; set; }
        public virtual string ColorCode { get; set; }
    }
}
