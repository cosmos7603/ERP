using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpDestination
    {
        [Key]
        public int GrpDestinationId { get; set; }
        public int GrpId { get; set; }
        public int DestinationId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
