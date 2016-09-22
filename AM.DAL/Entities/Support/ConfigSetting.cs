using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public class ConfigSetting : AuditableEntity
	{
		[Key]
		public int ConfigSettingId { get; set; }
		public string ConfigGroupCode { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Type { get; set; }
		public bool AllowAccessControl { get; set; }
	}
}
