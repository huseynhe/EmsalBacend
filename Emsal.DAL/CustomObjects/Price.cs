using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class Price
    {
        public decimal unit_price { get; set; }
        public decimal price { get; set; }
        public decimal totalPrice { get; set; }
        public Int64 productId { get; set; }
        public Int64 partOfYear { get; set; }



    }
}
