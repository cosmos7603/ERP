using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AirGateway : AuditableEntity
    {
        [Key]
        public int AirGatewayId { get; set; }
        public int StoreId { get; set; }
        public string AirportCode { get; set; }
        public string GatewayName { get; set; }
        public bool Deleted { get; set; }
    }
}
