using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Document : AuditableEntity
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentTypeCode { get; set; }
        public string DocumentStatus { get; set; }
        public string DocumentDescription { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int? FileSize { get; set; }
        public string FileType { get; set; }
        public string BrandCode { get; set; }
        public string ConfirmationNumber { get; set; }
        public short? ConfirmationTypeCode { get; set; }
        public int? VendorId { get; set; }
        public int? ResGrpId { get; set; }
        public bool? Grp { get; set; }
        public short? SpecialTypeCode { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime LastReviewedDate { get; set; }
        public int? StoreId { get; set; }
        public bool ShowInGrp { get; set; }
        public bool? EmailGenerated { get; set; }
        public string EmailFrom { get; set; }
        public bool? RestrictedGrpAccess { get; set; }
        public string EmailCode { get; set; }
        public string DocumentNote { get; set; }
        public bool? Duplicated { get; set; }
        public int? DuplicatedId { get; set; }
        public string HoCode { get; set; }
        public int? DataFileId { get; set; }
        public int? LeadId { get; set; }
        public int? CustomerId { get; set; }
    }
}
