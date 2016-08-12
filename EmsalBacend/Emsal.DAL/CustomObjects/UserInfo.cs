using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public  class UserInfo
    {
       public string name { get; set; }
       public string surname  { get; set; }
       public string fullAddress { get; set; }
       public string email { get; set; }
       public Int64 userID { get; set; }
       public Int64 userTypeID{ get; set; }
       public string userType { get; set; }
       public Int64 userRoleID { get; set; }
       public Int64 roleID { get; set; }
       public Int64 adminUnitID { get; set; }
       public string adminUnitName { get; set; }
       public string roleName { get; set; }
       public Int64 parentAdminUnitID { get; set; }
       public string parentAdminUnitName { get; set; }
       public string pinNumber { get; set; }
       public string voen { get; set; }
    }
}


