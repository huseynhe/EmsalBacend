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
    
    public partial class tblProductCatalog
    {
        public long Id { get; set; }
        public Nullable<long> ProductCatalogParentID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<long> canBeOrder { get; set; }
        public Nullable<long> CatalogLevel { get; set; }
        public Nullable<long> measurementUnit_enumValueId { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
    }
}
