using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerStatus
    {
        [Key]
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}
