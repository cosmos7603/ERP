using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string NotificationTypeCode { get; set; }
        public string CounselorLogin { get; set; }
        public int? MktgActivityId { get; set; }
        public int? ReportEmailExecutionId { get; set; }
        public DateTime NotificationDate { get; set; }
		public int? StoreNewsId { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
	}
}