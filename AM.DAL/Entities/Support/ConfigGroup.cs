using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public class ConfigGroup
	{
		[Key]
		public string ConfigGroupCode { get; set; }
		public string ConfigGroupName { get; set; }
	}
}
