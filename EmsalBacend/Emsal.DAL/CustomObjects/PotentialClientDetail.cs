using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public  class PotentialClientDetail
    {
       
        public string adminUnitName { get; set; }
        public string fromOrganisation { get; set; }
        public Int64 count { get; set; }
        public Int64 createdDate { get; set; }
    }
}
