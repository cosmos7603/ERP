using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class NCOAField
    {
        [Key]
        public string NcoaFieldCode { get; set; }
        public string NcoaFieldName { get; set; }
        public string ColumnHeader { get; set; }
    }
}
