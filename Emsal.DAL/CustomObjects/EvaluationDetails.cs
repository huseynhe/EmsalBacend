using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class EvaluationDetails
    {
        public long Id { get; set; }
        public Nullable<long> ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<long> EvumValId { get; set; }
        public Nullable<long> Point { get; set; }
        public Nullable<bool> IsMultiselect { get; set; }
        public Nullable<bool> IsAttachment { get; set; }
        public Nullable<bool> IsNote { get; set; }
        public Nullable<bool> IsQuestion { get; set; }
        public Nullable<bool> IsAnswer { get; set; }
        public Nullable<long> Sort { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
        public string enumValueName { get; set; }
        public string evaluationQuestionLocationDesc { get; set; }
        public string offerUserTypeDesc { get; set; }
        public string parentName { get; set; }
        public string number { get; set; }
        public Int64 offerUserTypeEValId { get; set; }
        public bool isRequired { get; set; }
        public Int64 evaluationQuestionLocationEValId { get; set; }
       
    }
}
