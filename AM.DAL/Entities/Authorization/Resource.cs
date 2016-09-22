using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    public partial class Resource : AuditableEntity
    {
        [Key]
		public string ResourceName { get; set; }
		public string MenuText { get; set; }
		public short? MenuSeqNum { get; set; }
		public string Path { get; set; }
		public string ModuleCode { get; set; }
		public bool? MenuSeparator { get; set; }
		public bool? CorpAccess { get; set; }
		public bool? StoreAccess { get; set; }
		public bool? CounselorAccess { get; set; }
		public string UserLevelFilter { get; set; }
		public bool Legacy { get; set; }
		public bool SubMenu { get; set; }
		public string ParentResourceName { get; set; }

		[ForeignKey("ModuleCode")]
		public virtual Module Module { get; set; }
		[ForeignKey("ResourceName")]
        public virtual List<Permission> Permissions { get; set; }
        [ForeignKey("ParentResourceName")]
        public virtual List<Resource> ChildResources { get; set; }

    }
}
