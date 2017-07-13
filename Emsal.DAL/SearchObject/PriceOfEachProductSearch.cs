using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
    [DataContract]
   public class PriceOfEachProductSearch
    {
        [DataMember(IsRequired = true)]
        public Int64 startDate { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 yearEvId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 endDate { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 year { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 partOfYear { get; set; }
    }
}
