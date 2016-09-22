using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class NCOAImport
    {
        [Key]
        public int NcoaImportId { get; set; }
        public int? ImportFileId { get; set; }
        public int? FailedExtractFileId { get; set; }
        public DateTime ImportDate { get; set; }
        public string ImportLogin { get; set; }
        public int? TotalRecords { get; set; }
        public int? UpdatedRecords { get; set; }
        public int? FailedRecords { get; set; }
        public bool Canceled { get; set; }
    }
}
