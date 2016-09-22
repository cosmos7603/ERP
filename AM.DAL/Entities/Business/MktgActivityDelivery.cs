using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class MktgActivityDelivery
    {
        [Key]
        public int MktgActivityDeliveryId { get; set; }
        public string ExternalEmailerCode { get; set; }
        public string ExternalMailingCode { get; set; }
        public int? CustomerId { get; set; }
        public string Email { get; set; }
        public string EventTypeCode { get; set; }
        public DateTime EventDate { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
        public bool Reviewed { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
