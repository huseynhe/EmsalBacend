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
    
    public partial class tblProductionControl
    {
        public long Id { get; set; }
        public Nullable<long> Demand_Production_Id { get; set; }
        public Nullable<long> Offer_Production_Id { get; set; }
        public Nullable<long> Potential_Production_Id { get; set; }
        public Nullable<long> Production_type_eV_Id { get; set; }
        public Nullable<long> EnumCategoryId { get; set; }
        public Nullable<long> EnumValueId { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createUser { get; set; }
        public Nullable<long> createDate { get; set; }
        public string updateUser { get; set; }
        public Nullable<long> updateDate { get; set; }
    }
}
