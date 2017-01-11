using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class ProductionCalendarDetail
    {

        public List<tblProductionCalendar> productionCalendarList { get; set; }

        public Int64 ID { get; set; }
        public Int64 production_Id { get; set; }
        public Int64 year { get; set; }
        public Int64 oclock { get; set; }
        public Int64 months_eV_Id { get; set; }
        public Int64 demand_Id { get; set; }
        public Int64 offer_Id { get; set; }
        public Int64 Production_type_eV_Id { get; set; }
        public Int64 partOfyear { get; set; }
        public Int64 transportation_eV_Id { get; set; }
        public Int64 type_eV_Id { get; set; }
        public Int64 day { get; set; }
        public decimal price { get; set; }
        public decimal quantity { get; set; }

        public String TypeName { get; set; }
        public string MonthName { get; set; }
        public string MonthDescription { get; set; }
        public string TypeDescription { get; set; }


    }
}
