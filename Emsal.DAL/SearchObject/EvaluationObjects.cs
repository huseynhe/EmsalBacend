using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Emsal.DAL.SearchObject
{
    [DataContract]
   public class EvaluationObjects
   {
       [DataMember(IsRequired = true)]
        public Int64 page { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 page_size { get; set; }
        [DataMember(IsRequired = true)]
        public string name { get; set; }
        [DataMember(IsRequired = true)]
        public string parentName { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 evaluationId { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 parentId { get; set; }
        [DataMember(IsRequired = true)]
        public string enumValueIDList { get; set; }
        [DataMember(IsRequired = true)]
        public Int64 userId { get; set; }
        [DataMember(IsRequired = true)]
        public string  groupId { get; set; }
        [DataMember(IsRequired = true)]
        public bool isApproved { get; set; }

         [DataMember(IsRequired = false)]
        public bool? isActive { get; set; }
         [DataMember(IsRequired = true)]
         public Int64 offerUserTypeEValId { get; set; }
         [DataMember(IsRequired = true)]
         public Int64 evaluationQuestionLocationEValId { get; set; }
    }
}
