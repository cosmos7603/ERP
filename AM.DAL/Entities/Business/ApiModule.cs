using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class ApiModule
    {
        [Key]
        public int ApiModuleId { get; set; }
        public string ApiModuleName { get; set; }
    }
}
