using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AgencyCard : AuditableEntity
    {
        [Key]
        public int AgencyCardId { get; set; }
        public int StoreId { get; set; }
        public string AgencyCardName { get; set; }
        public bool Deleted { get; set; }
        public string CodeLevelCode { get; set; }
        public string HoCode { get; set; }
    }
}
