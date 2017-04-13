using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
    [DataContract]
    public class PotensialUserForAdminUnitIdList1
    {
        [DataMember(IsRequired = true)]
        public string adminUnitName { get; set; }
        [DataMember(IsRequired = true)]
        public int page { get; set; }
        [DataMember(IsRequired = true)]
        public int pageSize { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 roleID { get; set; }
        [DataMember(IsRequired = true)]
        public string name { get; set; }
        [DataMember(IsRequired = true)]
        public string address { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 adminUnitID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 userID { get; set; }
        [DataMember(IsRequired = true)]
        public string pinNumber { get; set; }
        [DataMember(IsRequired = true)]
        public string voen { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 productID { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string nameSort { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string adminNameSort { get; set; }
    }
}
