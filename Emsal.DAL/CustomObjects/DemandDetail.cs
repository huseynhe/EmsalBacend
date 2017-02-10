using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class DemandDetail
    {
     
        public string productName { get; set; }
        public Int64 productionID { get; set; }
        public Int64 productID { get; set; }
        public Int64 userId { get; set; }
        public Int64 state_Ev_Id { get; set; }
        public string parentName { get; set; }
        public string fullAddress { get; set; }
        public string addressDesc { get; set; }
        public string kategoryName { get; set; }
        public tblDemand_Production demandList { get; set; }
        public List<ProductionCalendarDetail> calendarList { get; set; }
        public decimal totalQuantity { get; set; }
        public decimal totalPrice { get; set; }
        public decimal unit_price { get; set; }
        public string regionName { get; set; }
        public List<OfferProducts> offerList { get; set; }

    }
}
