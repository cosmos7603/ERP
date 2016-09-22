using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
    public class LeadCustomer : AuditableEntity
    {
		[Key]
        public int LeadId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerSeq { get; set; }
    }
}
