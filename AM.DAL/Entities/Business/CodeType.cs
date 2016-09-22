using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CodeType
    {
        [Key]
        public string CodeTypeCode { get; set; }
        public string CodeTypeName { get; set; }
        public bool AllowBrandLevel { get; set; }
    }
}
