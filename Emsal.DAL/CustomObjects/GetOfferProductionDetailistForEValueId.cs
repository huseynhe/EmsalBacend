using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class GetOfferProductionDetailistForEValueId
    {
        public Int64 productionID { get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }
        public string kategoryName { get; set; }
        public Int64 productId { get; set; }
        public string productName { get; set; }

        public string fullAddress { get; set; }
        public string organizationName { get; set; }

        public string productParentName { get; set; }
        public List<ProductionCalendarDetail> productionCalendarList { get; set; }
        public string name { get; set; }
        public string documentName { get; set; }
        public string documentUrl { get; set; }
        public string documentRealName { get; set; }
        public Int64 documentSize { get; set; }

        public string surname { get; set; }

        public string gender { get; set; }
        public string pinNumber { get; set; }
        public string email { get; set; }

        public string fatherName { get; set; }


        public string voen { get; set; }
        public List<tblCommunication> personcomList { get; set; }
        public Int64 personID { get; set; }

    }
}
