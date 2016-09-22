using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PromotionImport
    {
        [Key]
        public int Importid { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Rowsok { get; set; }
        public int? Rowsfailed { get; set; }
        public string Clientfilename { get; set; }
        public string Mappinginfo { get; set; }
        public bool? Exception { get; set; }
        public string Exceptiondescription { get; set; }
        public string Exceptionrow { get; set; }
        public string Createby { get; set; }
        public DateTime Createdate { get; set; }
    }
}
