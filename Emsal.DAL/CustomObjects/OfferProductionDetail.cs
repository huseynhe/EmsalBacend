using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    [DataContract]
    public class OfferProductionDetail
    {
        [DataMember(IsRequired = true)]
        public String adminName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 adminID { get; set; }
        [DataMember(IsRequired = true)]
        public string productName { get; set; }
        [DataMember(IsRequired = true)]
        public string productParentName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 productID { get; set; }
        [DataMember(IsRequired = true)]
        public decimal totalQuantity { get; set; }
        [DataMember(IsRequired = true)]
        public string quantityType { get; set; }
        [DataMember(IsRequired = true)]
        public string quantityTypeDescription { get; set; }
      

    }
}
