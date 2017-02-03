using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
     
   public class DemandOfferProductsSearch
    {
         
        public Int64 productId { get; set; }
       
        public string voen { get; set; }
        
        public string pinNumber { get; set; }
        
        public Int64 roleID { get; set; }
    }
}
