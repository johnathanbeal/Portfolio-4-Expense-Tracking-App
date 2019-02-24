using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YNAET.Exceptions
{
    public class NotFoundException : Exception
    {      
        public NotFoundException() : base()
        {
            
        }

        public override string Message
        {
            get
            {
                return "The record was not found";
            }
        }

    }
}
