using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AirSeg : AuditableEntity
    {
        [Key]
        public int AirSegId { get; set; }
        public int ResGrpId { get; set; }
        public bool Grp { get; set; }
        public short LineItemSeq { get; set; }
        public int CarrierId { get; set; }
        public string FlightNumber { get; set; }
        public int? DepGatewayId { get; set; }
        public DateTime? DepDate { get; set; }
        public bool? DepTransfer { get; set; }
        public int? ArrGatewayId { get; set; }
        public DateTime? ArrDate { get; set; }
        public bool? ArrTransfer { get; set; }
        public short? AirSegSeq { get; set; }
    }
}
