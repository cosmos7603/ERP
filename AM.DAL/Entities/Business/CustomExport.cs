using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomExport : AuditableEntity
    {
        [Key]
        public int CustomExportId { get; set; }
        public string CustomExportName { get; set; }
        public string ReportName { get; set; }
        public string CounselorLogin { get; set; }
    }
}
