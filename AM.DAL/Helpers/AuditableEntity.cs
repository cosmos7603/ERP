using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
	public class AuditableEntity
	{
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string EditBy { get; set; }
		public DateTime? EditDate { get; set; }
	}
}
