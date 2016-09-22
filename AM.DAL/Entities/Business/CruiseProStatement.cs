using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CruiseProStatement
    {
        [Key]
        public string CruiseproStatementCode { get; set; }
        public string CruiseproStatementName { get; set; }
        public string CwProductTypeCode { get; set; }
        public string CwItemTypeCode { get; set; }
        public bool IgnoreAmounts { get; set; }
        public int? CwItemSubtypeId { get; set; }
    }
}
