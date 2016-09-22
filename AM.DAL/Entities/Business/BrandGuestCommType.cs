using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class BrandGuestCommType
    {
        [Key, Column(Order = 0)]
        public int Guestcommtypeid { get; set; }
        [Key, Column(Order = 1)]
        public string Brandcode { get; set; }
    }
}
