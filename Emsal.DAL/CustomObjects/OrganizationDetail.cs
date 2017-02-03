using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class OrganizationDetail
    {
        public string fullAddress { get; set; }
        public string adminName { get; set; }
        public string adminName1 { get; set; }
        public string organizationName { get; set; }
        public string voen { get; set; }
        public string prodcutName { get; set; }
        public string unitOfMeasurement{ get; set; }
        public decimal unit_price { get; set; }
        public decimal quantity { get; set; }
        public Int64 personID { get; set; }
        public Int64 adminUNit_ID { get; set; }
        public string regionName { get; set; }
        public string managerName { get; set; }
        public string managerSurname { get; set; }
        public string parentProductName { get; set; }
        public string fatherName { get; set; }
        public string pinNumber { get; set; }
        public List<tblCommunication> comList { get; set; }
        public Int64 parentRegionID { get; set; }
        public Int64 prodcutID { get; set; }
        public Int64 organizationID { get; set; }
    }
}
