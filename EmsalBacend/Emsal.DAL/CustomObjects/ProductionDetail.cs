using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class ProductionDetail
    {
        //  User_id-ye gore tblPotential_Production ve tblDemand_Production cedvellerindeki isSelected-i
        // true olanlarin listini hem de (Məhsulun adı Ölçü vahidi Qiyməti Qrafik Aylar Region) parametrleri olan formada listi lazimdi
     
        public Int64 productionID { get; set; }
        public decimal unitPrice { get; set; }
        public decimal quantity { get; set; }
        public string description { get; set; }
        public Int64 productId { get; set; }
        public string productName { get; set; }
        public string Status { get; set; }
        public string fullAddress { get; set; }
        public string addressDesc { get; set; }
        public string enumValueName { get; set; }
        public Int64 enumCategoryId { get; set; }
        public Int64 enumValueId { get; set; }
        public Int64 userId { get; set; }
        public string organizationName { get; set; }
        public string groupId { get; set; }
        public List<string> months { get; set; }
        public tblPerson person { get; set; }
        public tblForeign_Organization foreignOrganization { get; set; }
        public List<tblProduction_Document> productionDocumentList { get; set; }
 
    }
}
