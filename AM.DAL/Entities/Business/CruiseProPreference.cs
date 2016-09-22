using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CruiseProPreference
    {
        [Key]
        public string CruiseproCode { get; set; }
        public string CodeTypeCode { get; set; }
        public int? CodeId { get; set; }
    }
}
