using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GuestCommPermission : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int GuestCommTypeId { get; set; }
        [Key, Column(Order = 1)]
        public int StoreId { get; set; }
        public string CounselorLogin { get; set; }
        public DateTime? OptInDate { get; set; }
        public DateTime? OptOutDate { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }
}
