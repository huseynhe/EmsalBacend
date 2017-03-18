using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class OfferPerson
    {
        public Int64 productionID { get; set; }
        public Int64 productID { get; set; }
        public string personName { get; set; }
        public string surname { get; set; }
        public string organizationName { get; set; }
        public string fatherName { get; set; }
        public string addresPerson { get; set; }
        public Int64 productOrigin { get; set; }
        public Int64 personID { get; set; }
        public string productAddress { get; set; }
        public string email { get; set; }
        public List<tblCommunication> comList { get; set; }
        public decimal unit_price { get; set; }
        public decimal unitPriceAnnouncement { get; set; }
    }
}
