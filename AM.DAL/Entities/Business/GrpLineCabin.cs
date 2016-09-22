using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpLineCabin : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int GrpId { get; set; }
        [Key, Column(Order = 1)]
        public short GrpLineItemSeq { get; set; }
        [Key, Column(Order = 2)]
        public string CabinNum { get; set; }
        public string CabinCatCode { get; set; }
        public string CabinSizeCode { get; set; }
        public bool Assigned { get; set; }
    }
}
