using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    public class BrandMktgSrc : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public string BrandCode { get; set; }
        [Key, Column(Order = 1)]
        public int MktgSrcId { get; set; }
    }
}
