using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
    [DataContract]
    public class DemandForegnOrganization
    {
        [DataMember(IsRequired = true)]
        public Int64 year { get; set; } 
        [DataMember(IsRequired = true)]
        public Int64 page { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 regionId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 addressID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 productID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 organizationID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 page_size { get; set; }
        
    }
}
