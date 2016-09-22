using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class MailListImport
    {
        [Key]
        public int ImportId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StoreId { get; set; }
        public string CounselorLogin { get; set; }
        public int? RowsOk { get; set; }
        public int? RowsFailed { get; set; }
        public string ClientFilename { get; set; }
        public int? MktgSrcId { get; set; }
        public bool? MailList { get; set; }
        public bool? OptIn { get; set; }
        public string MappingInfo { get; set; }
        public bool? ResultPending { get; set; }
        public bool? Exception { get; set; }
        public string ExceptionDescription { get; set; }
        public string ExceptionRow { get; set; }
        public bool? Canceled { get; set; }
        public string CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
