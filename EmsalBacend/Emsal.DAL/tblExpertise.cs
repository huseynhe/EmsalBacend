//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emsal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblExpertise
    {
        public long Id { get; set; }
        public string name { get; set; }
        public long Status { get; set; }
        public long LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public long createdDate { get; set; }
        public string updatedUser { get; set; }
        public long updatedDate { get; set; }
        public Nullable<int> expertiseType_enumCategoryId { get; set; }
        public Nullable<long> role_Id { get; set; }
    }
}