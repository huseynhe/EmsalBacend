﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmsalDBEntities : DbContext
    {
        public EmsalDBEntities()
            : base("name=EmsalDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<tblAddress> tblAddresses { get; set; }
        public virtual DbSet<tblAnnouncement> tblAnnouncements { get; set; }
        public virtual DbSet<tblAuthenticatedPart> tblAuthenticatedParts { get; set; }
        public virtual DbSet<tblBranchResponsibility> tblBranchResponsibilities { get; set; }
        public virtual DbSet<tblComMessage> tblComMessages { get; set; }
        public virtual DbSet<tblCommunication> tblCommunications { get; set; }
        public virtual DbSet<tblConfirmationMessage> tblConfirmationMessages { get; set; }
        public virtual DbSet<tblDemand_Production> tblDemand_Production { get; set; }
        public virtual DbSet<tblEmployee> tblEmployees { get; set; }
        public virtual DbSet<tblEnumCategory> tblEnumCategories { get; set; }
        public virtual DbSet<tblEnumValue> tblEnumValues { get; set; }
        public virtual DbSet<tblExpertise> tblExpertises { get; set; }
        public virtual DbSet<tblForeign_Organization> tblForeign_Organization { get; set; }
        public virtual DbSet<tblOffer_Production> tblOffer_Production { get; set; }
        public virtual DbSet<tblOrganization> tblOrganizations { get; set; }
        public virtual DbSet<tblParty> tblParties { get; set; }
        public virtual DbSet<tblPerson> tblPersons { get; set; }
        public virtual DbSet<tblPotential_Production> tblPotential_Production { get; set; }
        public virtual DbSet<tblPrivilegedRole> tblPrivilegedRoles { get; set; }
        public virtual DbSet<tblPRM_AdminUnit> tblPRM_AdminUnit { get; set; }
        public virtual DbSet<tblPRM_ASCBranch> tblPRM_ASCBranch { get; set; }
        public virtual DbSet<tblPRM_KTNBranch> tblPRM_KTNBranch { get; set; }
        public virtual DbSet<tblPRM_Thoroughfare> tblPRM_Thoroughfare { get; set; }
        public virtual DbSet<tblProduct_Document> tblProduct_Document { get; set; }
        public virtual DbSet<tblProductAddress> tblProductAddresses { get; set; }
        public virtual DbSet<tblProductCatalog> tblProductCatalogs { get; set; }
        public virtual DbSet<tblProductCatalogControl> tblProductCatalogControls { get; set; }
        public virtual DbSet<tblProduction_Calendar> tblProduction_Calendar { get; set; }
        public virtual DbSet<tblProduction_Document> tblProduction_Document { get; set; }
        public virtual DbSet<tblProductionControl> tblProductionControls { get; set; }
        public virtual DbSet<tblProductPrice> tblProductPrices { get; set; }
        public virtual DbSet<tblProductProfileImage> tblProductProfileImages { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblTitle> tblTitles { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserRole> tblUserRoles { get; set; }
    }
}
