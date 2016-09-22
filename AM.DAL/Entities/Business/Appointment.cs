using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Appointment : AuditableEntity
    {
        [Key]
        public int AppointmentId { get; set; }
        public int StoreId { get; set; }
        public string CounselorLogin { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTypeCode { get; set; }
        public string AppointmentDesc { get; set; }
        public bool Complete { get; set; }
        public int? GrpId { get; set; }
        public int? ReservationId { get; set; }
        public int? CustomerId { get; set; }
        public int? VendorId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime? DueDate { get; set; }
        public int? LineItemSeq { get; set; }
        public bool Canceled { get; set; }
        public int? FormLetterId { get; set; }
        public int? NoteTypeId { get; set; }
        public int? FollowupApptId { get; set; }
        public bool? HotMemo { get; set; }
        public int? CeActivityId { get; set; }
        public int? LeadId { get; set; }

	    public Customer Customer { get; set; }

    }
}
