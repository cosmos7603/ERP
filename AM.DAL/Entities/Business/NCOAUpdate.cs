using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class NCOAUpdate
    {
        [Key]
        public int NcoaUpdateId { get; set; }
        public int NcoaImportId { get; set; }
        public string NcoaFieldCode { get; set; }
        public int CustomerId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
