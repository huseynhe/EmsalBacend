using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.SearchObject
{
  public  class GetDemandProductionDetailistForEValueIdSearch
    {
       
        public int page { get; set; }
        public int pageSize { get; set; }
        public Int64 state_eV_Id { get; set; }
        public string person { get; set; }
        public string organizationName { get; set; }
        public Int64 prodcutID { get; set; }
    }
}
