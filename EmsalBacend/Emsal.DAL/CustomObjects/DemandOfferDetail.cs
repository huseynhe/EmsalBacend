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
    }
}
