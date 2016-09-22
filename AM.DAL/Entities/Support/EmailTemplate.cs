using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public partial class EmailTemplate
    {
		[Key]
		public string EmailTemplateCode { get; set; }
		public string EmailLayoutCode { get; set; }
		public string Subject { get; set; }
	}
}
