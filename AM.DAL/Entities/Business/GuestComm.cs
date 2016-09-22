using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GuestComm
    {
        [Key]
        public int GuestCommId { get; set; }
        public int GuestCommTypeId { get; set; }
        public int? Customerid { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string OptinTypeCode { get; set; }
        public DateTime? OptinDate { get; set; }
    }
}
