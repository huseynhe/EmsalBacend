﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.SearchObject
{
   public class PotensialUserForAdminUnitIdList
    {
    
       public string adminUnitName { get; set; }
       public int page{ get; set; }
       public int pageSize { get; set; }
       public Int64 roleID { get; set; }
       public string name { get; set; }
       public string address { get; set; }
       public Int64 adminUnitID { get; set; }
    }
}