using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
  public  class DemanOfferProduction
    {
        public Int64 productId { get; set; }
        public Int64 productionID { get; set; }
        public decimal totalquantity { get; set; }
        public decimal quantity { get; set; }
        public decimal unit_price { get; set; }
        public string roleName { get; set; }
        public string roledesc { get; set; }
        public string productName { get; set; }
        public string usertype { get; set; }
        public string personName { get; set; }
        public string surname { get; set; }
        public string voen { get; set; }
        public string pinNumber { get; set; }
        public string organizationName { get; set; }
        public Int64 personID { get; set; }
     
       // public string productName { get; set; }
        public string productParentName { get; set; }
        public decimal totalQuantityPrice { get; set; }
        public Int64  roleId { get; set; }
             
        public string enumValueName { get; set; }
        public List<tblCommunication> comList { get; set; }
        public string fatherName { get; set; }
     
    }
}
