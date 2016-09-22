using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
    public class ProductType : AuditableEntity
    {
		[Key]
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public string AgressoAccount { get; set; }
        public string AgressoType { get; set; }
        public int? PpoProductTypeId { get; set; }
    }
}
