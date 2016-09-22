using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CabinCat : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public short ShipId { get; set; }
        [Key, Column(Order = 1)]
        public string CabinCatCode { get; set; }
        [Key, Column(Order = 2)]
        public int StoreId { get; set; }
        public short? CabinCatSeq { get; set; }
        public string CabinDesc { get; set; }
        public bool Deleted { get; set; }
        public int? StateroomType { get; set; }
    }
}
