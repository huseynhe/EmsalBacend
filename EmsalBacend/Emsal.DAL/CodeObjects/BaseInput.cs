using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.CodeObjects
{
    [DataContract]
    public class BaseInput
    {
        [DataMember(IsRequired = true)]
        public string ChannelId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 userID { get; set; }
        [DataMember(IsRequired = true)]
        public String userName { get; set; }
        [DataMember(IsRequired = true)]
        public DateTime RequestDate { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 TransactionId { get; set; }

    }
}
