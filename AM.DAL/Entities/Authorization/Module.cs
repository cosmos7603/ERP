using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public partial class Module : AuditableEntity
    {
        [Key]
		public string ModuleCode { get; set; }
		public string ModuleName { get; set; }
		public byte MenuPosition { get; set; }
		public string Icon { get; set; }
    }
}
