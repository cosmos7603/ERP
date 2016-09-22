using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GuestCommAdjType
    {
        [Key]
        public string GuestCommAdjTypeCode { get; set; }
        public string GuestCommAdjTypeName { get; set; }
    }
}
