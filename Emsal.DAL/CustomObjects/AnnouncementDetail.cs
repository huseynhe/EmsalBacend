using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class AnnouncementDetail
    {
        //public AnnouncementDetail() {

        //    announcement = new tblAnnouncement();
        //    productCatalogDocumentList = new List<tblProduct_Document>();
        //}
        public tblAnnouncement announcement { get; set; }
        public string productName { get; set; }
        public List<tblProduct_Document> productCatalogDocumentList { get; set; }
        public String parentName { get; set; }
        public Int64 productId { get; set; }
        public string description { get; set; }
        public Int64 enumValueID { get; set; }
        public Int64 announcementID { get; set; }
        public List<tblAnnouncement> announcementlist { get; set; }
      
       
    }
}
