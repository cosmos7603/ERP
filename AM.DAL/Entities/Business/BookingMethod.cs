using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class BookingMethod
    {
        [Key]
        public string BookingMethodCode { get; set; }
        public string BookingMethodName { get; set; }
    }
}
