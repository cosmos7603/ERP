using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CruiseTourSeg : AuditableEntity
    {
        [Key]
        public int CruiseTourSegId { get; set; }
        public int ResGrpId { get; set; }
        public bool Grp { get; set; }
        public short LineItemSeq { get; set; }
        public short CruiseTourSegSeq { get; set; }
        public string SegType { get; set; }
        public string SegDesc { get; set; }
    }
}
