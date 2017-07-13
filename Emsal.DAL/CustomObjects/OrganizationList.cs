using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class OrganizationList
        
    {
        public Int64 orgID { get; set; }
        public string orgName { get; set; }
        public string reionName { get; set; }
        public Int64 productCount { get; set; }
        public string fullAddress { get; set; }
        public string addressDesc { get; set; }
        public List<Organizations> orgList { get; set; }
        public string username { get; set; }
        public string enumValueYear { get; set; }
    }
}
