using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class DemandOfferDetail
    {
        public string adminUnittName { get; set; }
        public Int64 productID { get; set; }
        public Int64 count { get; set; }
        public string productName { get; set; }
        public string productType { get; set; }
        public string productParentName { get; set; }
        public string name { get; set; }
        public decimal difference { get; set; }
        public decimal unitPrice { get; set; }
        public decimal quantity { get; set; }
        public string description { get; set; }
        public string kategoryName { get; set; }
        public decimal offerDemand { get; set; }
        public decimal demandQuantity { get; set; }
        public string type { get; set; }
        public Int64 userId { get; set; }
        public Int64 enumKategoryID { get; set; }
    }
}
