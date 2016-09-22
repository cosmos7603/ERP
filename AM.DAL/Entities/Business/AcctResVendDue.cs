using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AcctResVendDue
    {
        [Key, Column(Order = 0)]
        public int ResGrpId { get; set; }
        [Key, Column(Order = 1)]
        public bool Grp { get; set; }
        [Key, Column(Order = 2)]
        public string CurrencyCode { get; set; }
        [Key, Column(Order = 3)]
        public int VendorId { get; set; }
        [Key, Column(Order = 4)]
        public DateTime DueDate { get; set; }
        public decimal? DueAmt { get; set; }
        public decimal? OwedAmt { get; set; }
        public bool FnlPymt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
