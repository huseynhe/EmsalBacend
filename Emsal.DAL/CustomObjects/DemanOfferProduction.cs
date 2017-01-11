using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
  public  class DemanOfferProduction
    {
        public Int64 productId { get; set; }
        public Int64 productParentId { get; set; }
        public string productName { get; set; }      
       // public string productName { get; set; }
        public string productParentName { get; set; }
        public decimal demandQuantity { get; set; }
        public decimal offerQuantity { get; set; }
        public decimal diffQuantity { get; set; }

        public string description { get; set; }
             
        public string enumValueName { get; set; }
        public Int64 enumCategoryId { get; set; }
        public Int64 enumValueId { get; set; }
     
    }
}
