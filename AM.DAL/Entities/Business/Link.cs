using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Link : AuditableEntity
    {
        [Key]
        public int LinkId { get; set; }
        public string BrandCode { get; set; }
        public string LinkName { get; set; }
        public string LinkGroupCode { get; set; }
        public string LinkUrl { get; set; }
        public byte LinkSeq { get; set; }
        public bool Deleted { get; set; }
        public bool? PointToStoreWebsite { get; set; }
        public int? StoreId { get; set; }
    }
}
