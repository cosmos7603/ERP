using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GuestCommHst
    {
        [Key]
        public int GuestCommHstId { get; set; }
        public int GuestCommTypeId { get; set; }
        public int? CustomerId { get; set; }
        public string OptInTypeCode { get; set; }
        public DateTime? OptInDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
