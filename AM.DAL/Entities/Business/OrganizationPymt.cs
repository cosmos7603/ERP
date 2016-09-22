using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class OrganizationPymt
    {
        [Key]
        public int OrganizationPymtId { get; set; }
        public int TransactionId { get; set; }
        public byte TransactionSeq { get; set; }
        public decimal PaymentAmt { get; set; }
    }
}
