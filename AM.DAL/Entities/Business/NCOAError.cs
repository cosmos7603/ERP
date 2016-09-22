using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class NCOAError
    {
        [Key]
        public int NcoaErrorId { get; set; }
        public int NcoaImportId { get; set; }
        public int RowIndex { get; set; }
        public string ErrorMessage { get; set; }
    }
}
