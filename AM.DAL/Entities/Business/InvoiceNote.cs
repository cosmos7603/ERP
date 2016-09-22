using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class InvoiceNote : AuditableEntity
    {
        [Key]
        public int InvoiceNoteId { get; set; }
        public int StoreId { get; set; }
        public string InvoiceNoteDesc { get; set; }
        public bool Deleted { get; set; }
        public string CounselorLogin { get; set; }
        public int? ReservationId { get; set; }
        public string NoteLevelCode { get; set; }
        public int? GrpId { get; set; }
    }
}
