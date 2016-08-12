using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
    public class AnnouncementDetail
    {
        public AnnouncementDetail() {
            announcement = new tblAnnouncement();
            productCatalogDocumentList = new List<tblProduct_Document>();
        }
        public tblAnnouncement announcement { get; set; }
        public List<tblProduct_Document> productCatalogDocumentList { get; set; }
    }
}
