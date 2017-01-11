using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class ProductCatalogDetail
    {
        public ProductCatalogDetail()
        {

            productCatalogDocumentList = new List<tblProduct_Document>();
        }
        public tblProductCatalog productCatalog { get; set; }
        public tblProductCatalog parentProductCatalog { get; set; }
        public string productName { get; set; }
        public List<tblProduct_Document> productCatalogDocumentList { get; set; }

    }
}
