using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LeadStatus
    {
        [Key]
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}
