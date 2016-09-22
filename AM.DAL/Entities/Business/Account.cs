using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{ 
    public class Account
    {
        [Key]
        public string Accountcode { get; set; }
        public string Accountname { get; set; }
    }
}
