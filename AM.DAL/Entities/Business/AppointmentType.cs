using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AppointmentType
    {
        [Key]
        public string AppointmentTypeCode { get; set; }
        public string AppointmentTypeName { get; set; }
    }
}
