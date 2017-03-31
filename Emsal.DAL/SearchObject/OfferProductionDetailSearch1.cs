using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Text;
namespace Emsal.DAL.SearchObject
{
    [DataContract]
    public class OfferProductionDetailSearch1
    {
        [DataMember(IsRequired = true)]
        public Int64 monintoring_eV_Id { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 adminID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 productID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 roleID { get; set; }
        [DataMember(IsRequired = true)]
        public int page { get; set; }
        [DataMember(IsRequired = true)]
        public int pageSize { get; set; }
        [DataMember(IsRequired = true)]
        public string name { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 state_eV_Id { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 userID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 contractID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 personID { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 usertypeEvId { get; set; }
        [DataMember(IsRequired = true)]
        public string voen { get; set; }
        [DataMember(IsRequired = true)]
        public string pinNumber { get; set; }
        [DataMember(IsRequired = true)]
        public string productName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 countryId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 startDate { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 endDate { get; set; }
    }
}
