using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public partial class State
    {   
        [Key]
		public string CountryCode { get; set; }
		public string StateCode { get; set; }
		public string StateName { get; set; }

	}
}
