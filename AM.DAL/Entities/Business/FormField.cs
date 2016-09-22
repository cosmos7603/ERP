using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class FormField
    {
        [Key]
        public int FormFieldId { get; set; }
        public string FieldName { get; set; }
        public string InsertText { get; set; }
        public byte OrderSeq { get; set; }
    }
}
