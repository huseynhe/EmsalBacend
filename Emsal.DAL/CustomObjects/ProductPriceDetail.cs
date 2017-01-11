using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class ProductPriceDetail
    {
        public List<tblProductPrice> productPriceList { get; set; }
        public Int64 productID { get; set; }
        public Int64 partOfYear { get; set; }
        public string productName { get; set; }
        public string productParentName { get; set; }
        public Int64 priceID { get; set; }
        public decimal unit_price { get; set; }
        public Int64 year { get; set; }
        public Int64 canBeOrder { get; set; }
        public String ProductDescription { get; set; }
    }
}
