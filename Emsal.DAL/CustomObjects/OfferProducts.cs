using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class OfferProducts
    {
        public Int64 productID { get; set; }
        public string productName { get; set; }
        public string parentName { get; set; }
        public decimal totalQuantity { get; set; }
        public decimal totalPrice { get; set; }
        public Int64 roleId { get; set; }
        public Int64 monthID { get; set; }
        public string type { get; set; }
    }
}
