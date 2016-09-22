using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpLinePricing : AuditableEntity
    {
        [Key]
        public int GrpLinePricingId { get; set; }
        public int GrpId { get; set; }
        public short GrpLineItemSeq { get; set; }
        public string CatCode { get; set; }
        public decimal TotalQty { get; set; }
        public decimal AllocQty { get; set; }
        public decimal PsBrochureAmt { get; set; }
        public decimal PsVendorAmt { get; set; }
        public decimal PsStoreAmt { get; set; }
        public decimal PsCommPrct { get; set; }
        public decimal Pd1BrochureAmt { get; set; }
        public decimal Pd1VendorAmt { get; set; }
        public decimal Pd1StoreAmt { get; set; }
        public decimal Pd1CommPrct { get; set; }
        public decimal? Pd2BrochureAmt { get; set; }
        public decimal Pd2VendorAmt { get; set; }
        public decimal Pd2StoreAmt { get; set; }
        public decimal Pd2CommPrct { get; set; }
        public decimal P34BrochureAmt { get; set; }
        public decimal P34VendorAmt { get; set; }
        public decimal P34StoreAmt { get; set; }
        public decimal P34CommPrct { get; set; }
        public decimal? PsTaxFeeAmt { get; set; }
        public decimal? PsGovFeeAmt { get; set; }
        public decimal? PsOtherCommAmt { get; set; }
        public decimal? Pd1TaxFeeAmt { get; set; }
        public decimal? Pd1GovFeeAmt { get; set; }
        public decimal? Pd1OtherCommAmt { get; set; }
        public decimal? Pd2TaxFeeAmt { get; set; }
        public decimal? Pd2GovFeeAmt { get; set; }
        public decimal? Pd2OtherCommAmt { get; set; }
        public decimal? P34TaxFeeAmt { get; set; }
        public decimal? P34GovFeeAmt { get; set; }
        public decimal? P34OtherCommAmt { get; set; }
        public int? GrpSegmentId { get; set; }
        public bool Fit { get; set; }
        public bool Pd2Publish { get; set; }
        public bool P34Publish { get; set; }
        public bool PsPublish { get; set; }
        public string PublishType { get; set; }
        public decimal? PsGstCommAmt { get; set; }
        public decimal Pd2GstCommAmt { get; set; }
        public decimal? P34GstCommAmt { get; set; }
        public decimal? Pd1GstCommAmt { get; set; }
        public decimal? PsMarkupDsctAmt { get; set; }
        public decimal? PsCommAmt { get; set; }
        public decimal? PsVendorCostAmt { get; set; }
        public decimal? Pd1MarkupDsctAmt { get; set; }
        public decimal? Pd1CommAmt { get; set; }
        public decimal? Pd1VendorCostAmt { get; set; }
        public decimal? Pd2MarkupDsctAmt { get; set; }
        public decimal? Pd2CommAmt { get; set; }
        public decimal? Pd2VendorCostAmt { get; set; }
        public decimal? P34MarkupDsctAmt { get; set; }
        public decimal? P34CommAmt { get; set; }
        public decimal? P34VendorCostAmt { get; set; }
        public decimal? FreeQty { get; set; }
    }
}
