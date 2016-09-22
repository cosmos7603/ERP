using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpSegment : AuditableEntity
    {
        [Key]
        public int GrpSegmentId { get; set; }
        public int GrpId { get; set; }
        public string GrpSegmentName { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Deleted { get; set; }
        public string EmbarkCityName { get; set; }
        public string DebarkCityName { get; set; }
        public string GrpSegmentGroupId { get; set; }
        public byte TcRatio { get; set; }
    }
}
