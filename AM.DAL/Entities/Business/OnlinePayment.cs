using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class OnlinePayment
    {
        [Key]
        public int OnlinePaymentId { get; set; }
        public int? ReservationId { get; set; }
        public decimal PaymentAmt { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CcTypeCode { get; set; }
        public string CcNum { get; set; }
        public string CcExpDate { get; set; }
        public string CcName { get; set; }
        public bool KeepInsurance { get; set; }
        public bool SendInvoice { get; set; }
        public bool Pending { get; set; }
        public string ConfirmBy { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Declined { get; set; }
        public int? PassengerId { get; set; }
        public string CcCvv { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public string CreateBy { get; set; }
        public int? GrpRegId { get; set; }
        public string BillingAddress { get; set; }
        public int? PayorId { get; set; }
    }
}
