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
    
    public partial class tblProductAddress
    {
        public long Id { get; set; }
        public Nullable<long> adminUnit_Id { get; set; }
        public Nullable<long> thoroughfare_Id { get; set; }
        public string fullAddress { get; set; }
        public string addressDesc { get; set; }
        public Nullable<long> demand_production_id { get; set; }
        public Nullable<long> offer_production_id { get; set; }
        public Nullable<long> potensial_production_id { get; set; }
        public Nullable<long> production_type_ev_Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
        public string fullAddressId { get; set; }
        public string fullForeignOrganization { get; set; }
        public Nullable<long> forgId { get; set; }
    }
}
