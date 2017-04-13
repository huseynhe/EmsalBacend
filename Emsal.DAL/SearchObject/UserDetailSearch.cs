using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
    [DataContract]
    public class UserDetailSearch
    {
        [DataMember(IsRequired = true)]
        public Int64 usertype_Ev_Id { get; set; }
        [DataMember(IsRequired = true)]
        public string name { get; set; }
        [DataMember(IsRequired = true)]
        public string username { get; set; }
        [DataMember(IsRequired = true)]
        public string email { get; set; }
        [DataMember(IsRequired = true)]
        public int page { get; set; }
        [DataMember(IsRequired = true)]
        public int pageSize { get; set; }
        [DataMember(IsRequired = true)]
        public string fullAddress { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 roleID { get; set; }
        [DataMember(IsRequired = true)]
        public string fatherName { get; set; }
        [DataMember(IsRequired = true)]
        public string surName { get; set; }
        [DataMember(IsRequired = true)]
        public string organizationName { get; set; }
        [DataMember(IsRequired = true)]
        public bool parentID { get; set; }

    }
}
