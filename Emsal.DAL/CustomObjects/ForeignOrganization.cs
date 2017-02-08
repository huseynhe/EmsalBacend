using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class ForeignOrganization
    {
        public Int64 ID { get; set; }
        public string organizationName { get; set; }
        
        public List<AdminUnitRegion> adminUnitIdList { get; set; }
    }
}
