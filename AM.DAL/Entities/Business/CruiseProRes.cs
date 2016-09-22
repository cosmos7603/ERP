using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CruiseProRes
    {
        [Key]
        public int CruiseproResId { get; set; }
        public string AuSyncid { get; set; }
        public DateTime PriActBeginDate { get; set; }
        public int PpoShipId { get; set; }
        public string CatCode { get; set; }
        public string PriPaxName { get; set; }
        public string XmlData { get; set; }
        public bool Pending { get; set; }
        public DateTime CreateDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public int PpoVendorId { get; set; }
        public string ConfirmationNumber { get; set; }
        public DateTime PriActEndDate { get; set; }
        public DateTime? VndrDpstDate { get; set; }
        public DateTime? VndrFnlPymtDate { get; set; }
        public string XmlPricings { get; set; }
        public bool Deleted { get; set; }
    }
}
