using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
    [DataContract]
    public class GetDemandProductionDetailistForEValueIdSearch
    {
        [DataMember(IsRequired = true)]
        public int page { get; set; }
        [DataMember(IsRequired = true)]
        public int pageSize { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 state_eV_Id { get; set; }
        [DataMember(IsRequired = true)]
        public string Name { get; set; }
        [DataMember(IsRequired = true)]
        public string organizationName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 prodcutID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 roleID { get; set; }
        [DataMember(IsRequired = true)]
        public string voen { get; set; }
        [DataMember(IsRequired = true)]
        public string pinNumber { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 contractId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 monitoring_Ev_Id { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 user_Id { get; set; }

    }
}
