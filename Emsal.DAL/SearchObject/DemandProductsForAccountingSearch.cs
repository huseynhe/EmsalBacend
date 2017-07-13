using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
      [DataContract]
  public  class DemandProductsForAccountingSearch
    {
        [DataMember(IsRequired = true)]
        public string productName { get; set; }
        [DataMember(IsRequired = true)]
        public string adressName { get; set; }
        [DataMember(IsRequired = true)]
        public int page_num { get; set; }
        [DataMember(IsRequired = true)]
        public int page_size { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 state_eV_Id { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 roleID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 userType_eV_ID { get; set; }
        [DataMember(IsRequired = true)]
        public string parentName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 userID { get; set; }
       
        [DataMember(IsRequired = true)]
        public Int64 yearEvId { get; set; }
    }
}
