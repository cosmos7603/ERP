using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GuestCommAdj
    {
        [Key]
        public int GuestCommAdjId { get; set; }
        public int GuestCommTypeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime GuestCommAdjDate { get; set; }
        public string GuestCommAdjTypeCode { get; set; }
        public string GuestCommOptType { get; set; }
        public bool GuestCommOpt { get; set; }
        public string EmailName { get; set; }
        public string IpAddress { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
