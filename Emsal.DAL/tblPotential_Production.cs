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
    
    public partial class tblPotential_Production
    {
        public long Id { get; set; }
        public string grup_Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Nullable<decimal> unit_price { get; set; }
        public Nullable<decimal> total_price { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public Nullable<long> quantity_type_eV_Id { get; set; }
        public Nullable<long> startDate { get; set; }
        public Nullable<long> endDate { get; set; }
        public Nullable<bool> isSelected { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
        public Nullable<long> product_Id { get; set; }
        public Nullable<long> productAddress_Id { get; set; }
        public Nullable<long> user_Id { get; set; }
        public Nullable<long> state_eV_Id { get; set; }
        public string fullProductId { get; set; }
        public Nullable<long> monitoring_eV_Id { get; set; }
    }
}
