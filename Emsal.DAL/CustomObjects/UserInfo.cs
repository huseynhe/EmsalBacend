using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class UserInfo
    {

        public UserInfo()
        {

            productCatalogDetailList = new List<ProductCatalogDetail>();
        }

        public Int64 personId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string fullAddress { get; set; }
        public string email { get; set; }
        public Int64 userID { get; set; }
        public Int64 userTypeID { get; set; }
        public string userType { get; set; }
        public Int64 userRoleID { get; set; }
        public Int64 roleID { get; set; }
        public Int64 adminUnitID { get; set; }
        public string adminUnitName { get; set; }
        public string roleName { get; set; }
        public Int64 parentAdminUnitID { get; set; }
        public string parentAdminUnitName { get; set; }
        public string pinNumber { get; set; }
        public string voen { get; set; }
        public string fatherName { get; set; }
        public string gender { get; set; }
        public Int64 birtday { get; set; }
        public Int64 state_Ev_ID { get; set; }
        public Int64 ID { get; set; }
     
        public string profilPicture { get; set; }
        public Int64 adress_Id { get; set; }
        public string potentialProduct { get; set; }
        public decimal potentialProductQuantity { get; set; }
        public string personAdress { get; set; }
        public string personAdressDesc { get; set; }
        public string parantName { get; set; }
        public decimal productUnitPrice { get; set; }
        public Int64 month_Ev_Id { get; set; }

        public Int64 year { get; set; }

        public Int64 day { get; set; }
        public Int64 createdDate { get; set; }
        public Int64 updatedDate { get; set; }
        public Int64 lastUpdateStatus { get; set; }
        public string roleDescription { get; set; }
        public string OrganisationName { get; set; }
        public List<PotesialProducts> demandDetail { get; set; }
        public List<ProductCatalogDetail> productCatalogDetailList { get; set; }
        public string phoneNumber { get; set; }
        public string phoneNumberDesc { get; set; }
        public List<tblCommunication> personcomList { get; set; }
    }
}


