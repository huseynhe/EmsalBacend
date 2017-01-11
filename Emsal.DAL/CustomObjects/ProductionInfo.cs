using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class ProductionInfo
    {
        public List<ProductionDetail> productionDetailList { get; set; }
        public tblPerson person { get; set; }
        public List<tblProduction_Document> productionDocumentList { get; set; }
    }
}
