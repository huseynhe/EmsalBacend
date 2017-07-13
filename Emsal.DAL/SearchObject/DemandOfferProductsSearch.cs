using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{

    [DataContract]
    public class DemandOfferProductsSearch
    {
        
        [DataMember(IsRequired = true)]
        public Int64 productId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 yearEvId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 adminID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 regionID { get; set; }
        [DataMember(IsRequired = true)]
        public string voen { get; set; }
        [DataMember(IsRequired = true)]
        public string pinNumber { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 roleID { get; set; }
        [DataMember(IsRequired = true)]
        public int page { get; set; }
        [DataMember(IsRequired = true)]
        public int page_size { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 monthID { get; set; }
        [DataMember(IsRequired = true)]
        public string organizationName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 orgID { get; set; }
        [DataMember(IsRequired=true)]
        public Int64 stateEvId { get; set; }
    }
}
