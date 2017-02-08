using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class AdminUnitRegion
    {
        public Int64 ID { get; set; }
        public string name { get; set; }
        public string regionName { get; set; }
        public tblPRM_AdminUnit adminUNitList { get; set; }
      
    }
}
