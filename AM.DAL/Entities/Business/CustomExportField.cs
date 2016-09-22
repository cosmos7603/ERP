using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomExportField
    {
        [Key]
        public int CustomExportFieldId { get; set; }
        public int CustomExportId { get; set; }
        public int FieldSeq { get; set; }
        public string FieldName { get; set; }
    }
}
