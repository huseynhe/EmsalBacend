using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class DemandDetialOPA
    {
        public Int64 productionID { get; set; }
        public string productParentName { get; set; }
        public decimal unitPrice { get; set; }
        public decimal quantity { get; set; }
        public string productName { get; set; }
        public string fullAddress { get; set; }
        public string addressDesc { get; set; }
        public string enumValueName { get; set; }
        public string organizationName { get; set; }
        public string enumValueDescription { get; set; }
        public List<ProductionCalendarDetail> productionCalendarList { get; set; }


    }
}
