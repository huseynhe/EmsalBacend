//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emsal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblForeign_Organization
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long Status { get; set; }
        public long LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public long createdDate { get; set; }
        public string updatedUser { get; set; }
        public long updatedDate { get; set; }
        public Nullable<long> address_Id { get; set; }
        public string voen { get; set; }
        public Nullable<long> userId { get; set; }
        public Nullable<long> manager_Id { get; set; }
        public Nullable<long> organisationType_eV_Id { get; set; }
        public Nullable<long> parent_Id { get; set; }
        public Nullable<long> party_Id { get; set; }
        public Nullable<long> actualFunctionAddressId { get; set; }
        public Nullable<long> legalAddressId { get; set; }
        public Nullable<long> accountNumber { get; set; }
        public string bankName { get; set; }
        public Nullable<long> legality_eV_Id { get; set; }
    }
}
