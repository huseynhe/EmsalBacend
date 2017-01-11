using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class ProductionDetail
    {
        //  User_id-ye gore tblPotential_Production ve tblDemand_Production cedvellerindeki isSelected-i
        // true olanlarin listini hem de (Məhsulun adı Ölçü vahidi Qiyməti Qrafik Aylar Region) parametrleri olan formada listi lazimdi

        public Int64 productionID { get; set; }
        public Int64 stateID { get; set; }
       
        public decimal unitPrice { get; set; }
        public decimal quantity { get; set; }
        public string description { get; set; }
        public string kategoryName { get; set; }
        public Int64 productId { get; set; }
        public Int64 productParentId { get; set; }
        
        public Int64 priceProductId { get; set; }
        public string productName { get; set; }
        public string Status { get; set; }
        public string fullAddress { get; set; }
        public string addressDesc { get; set; }
        public string enumValueName { get; set; }
        public string enumValueDescription { get; set; }
        public Int64 enumCategoryId { get; set; }
        public Int64 enumValueId { get; set; }
        public Int64 userId { get; set; }
        public string organizationName { get; set; }
        public string groupId { get; set; }
        public List<string> months { get; set; }
        public tblPerson person { get; set; }
        public string productParentName { get; set; }
        public string grup_id { get; set; }
        //public int demand_id { get; set; }
        public tblForeign_Organization foreignOrganization { get; set; }
        public List<tblProduction_Document> productionDocumentList { get; set; }
        public List<ProductionCalendarDetail> productionCalendarList { get; set; }
        public List<tblProduct_Document> productDocumentList { get; set; }
        public string fullForeignOrganization { get; set; }
        public Nullable<long> forgId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public Int64 birtday { get; set; }
        public string gender { get; set; }
        public string pinNumber { get; set; }
        public string email { get; set; }
        public string communication { get; set; }
        public string fatherName { get; set; }
        public string profilPicture { get; set; }
        public Int64 adress_Id { get; set; }
        public string potentialProduct { get; set; }
        public decimal potentialProductQuantity { get; set; }
        public string personAdress { get; set; }
        public string personAdressDesc { get; set; }
        public decimal productUnitPrice { get; set; }
        public Int64 month_Ev_Id { get; set; }

        public Int64 year { get; set; }

        public Int64 day { get; set; }
        public Int64 createdDate { get; set; }
        public Int64 updatedDate { get; set; }
        public Int64 lastUpdateStatus { get; set; }
        public Int64 transportation_eV_Id { get; set; }
        public decimal totalPrice { get; set; }
        public decimal potentialQuantity { get; set; }
        public Int64 productOrigin { get; set; }
        public string userType { get; set; }
        public string organizationManagerName { get; set; }
        public string  productOriginName { get; set; }
        public Int64 contractID { get; set; }
        public Int64 roleID { get; set; }
        public Int64 managerId { get; set; }
        public Int64 organizationId { get; set; }
        public Int64 userType_eV_ID { get; set; }
        public string roleName { get; set; }
        public PersonInformation personInformation { get; set; }
        public string voen { get; set; }
        public List<tblCommunication> personcomList { get; set; }
        public Int64 personID { get; set; }
        public Int64 productAddressID { get; set; }
        public string phoneNumber { get; set; }
        public string roleDescription { get; set; }
    }
}
