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
    
    public partial class tblContract
    {
        public long Id { get; set; }
        public string ContractNumber_ { get; set; }
        public Nullable<long> AgentUserID { get; set; }
        public Nullable<long> SupplierOrganisationID { get; set; }
        public Nullable<long> SupplierUserID { get; set; }
        public Nullable<long> Status { get; set; }
        public string documentUrl { get; set; }
        public string documentName { get; set; }
        public string documentRealName { get; set; }
        public Nullable<long> documentSize { get; set; }
        public string ContractTitle { get; set; }
        public string ContractDescription { get; set; }
        public string Contract_type_ev_Id { get; set; }
        public Nullable<long> ContractStartDate { get; set; }
        public Nullable<long> ContractEndDate { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
    }
}
