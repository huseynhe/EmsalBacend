using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class PersonDetail
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FatherName { get; set; }
        public string voen { get; set; }
        public string pinNumber { get; set; }
        public string organizationName { get; set; }
        public Int64 personId { get; set; }
        public string profilePicture { get; set; }
        public string gender { get; set; }
        public Int64 userId { get; set; }
    
        public tblContract contractList { get; set; }
        public Int64 contractID { get; set; }
        public bool contractStatus { get; set; }
    }
}
