using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class DemandPrice
    {
        public Int64 product_id { get; set; }
        public Int64 transportation_eV_Id { get; set; }
        public decimal quantity { get; set; }
        public Int64 partOfYear { get; set; }
    }
}
