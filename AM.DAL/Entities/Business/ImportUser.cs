using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class ImportUser
    {
        [Key, Column(Order = 0)]
        public int Storeid { get; set; }
        [Key, Column(Order = 1)]
        public string Cw5userid { get; set; }
        public string Cw6login { get; set; }
    }
}
