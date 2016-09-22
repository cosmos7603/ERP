using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Branch : AuditableEntity
    {
        [Key]
        public int BranchId { get; set; }
        public int? StoreId { get; set; }
        public string BranchName { get; set; }
        public bool Active { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool? UseNameOnInvoice { get; set; }
        public int? InvoiceLogoId { get; set; }
        public int? IataId { get; set; }
        public string SproutloudAccountCode { get; set; }
        public string TanId { get; set; }
        public string ShoreExcGroupId { get; set; }
        public string BranchDba { get; set; }
        public bool MktgOptInEmail { get; set; }
        public bool MktgOptInPrinted { get; set; }
        public string MktgOptInPrintType { get; set; }
        public int? EngagementLogoId { get; set; }
        public string GrpLandingPageUrl { get; set; }
        public bool GrpEnableRoommateMatching { get; set; }
        public string GrpRoommateDisclosure { get; set; }
        public string GrpCustomConfirmUrl { get; set; }
        public string PromoPagePreviewUrl { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
        [ForeignKey("IataId")]
        public virtual IATA Iata { get; set; }
    }
}
