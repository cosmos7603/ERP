using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Ad : AuditableEntity
    {
        [Key]
        public int AdId { get; set; }
        public string HoCode { get; set; }
        public string AdName { get; set; }
        public string AdUrl { get; set; }
        public string AdLocationCode { get; set; }
        public int? AdFileId { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("AdLocationCode")]
        public virtual AdLocation AdLocation { get; set; }

        public virtual List<AdBrand> AdBrand { get; set; }

        public Ad()
        {
            AdBrand = new List<AdBrand>();
        }
    }
}
