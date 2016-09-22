using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpType
    {
        [Key]
        public string GrpTypeCode { get; set; }
        public string GrpTypeName { get; set; }
        public byte? OrderSeq { get; set; }
    }
}
