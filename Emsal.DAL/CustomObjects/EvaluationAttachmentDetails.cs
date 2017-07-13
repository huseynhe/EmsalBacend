using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
  public  class EvaluationAttachmentDetails
    {
        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> EvaluationId { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
        public string documentUrl { get; set; }
        public string documentName { get; set; }
        public string documentRealName { get; set; }
        public Nullable<long> documentSize { get; set; }
        public string document_type_ev_Id { get; set; }
        public string groupId { get; set; }
        public Nullable<bool> isApproved { get; set; }
    }
}
