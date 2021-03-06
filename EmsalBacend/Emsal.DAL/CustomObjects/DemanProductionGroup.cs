﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class DemanProductionGroup
    {
        public Int64  startDate { get; set; }
        public Int64  endDate { get; set; }
        public Int64  productId{ get; set; }
        public string productName { get; set; }
        public decimal totalQuantity { get; set; }
        public decimal unitPrice { get; set; }
        public Int64 enumValuId { get; set; }
        public string enumValueName { get; set; }
    }
}
