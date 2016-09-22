using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
    public class MktgCat : AuditableEntity
    {
        [Key]
        public short MktgCatId { get; set; }
        public string MktgCatName { get; set; }
    }
}