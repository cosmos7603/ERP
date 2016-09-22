using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    public partial class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipients { get; set; }
        public string Sender { get; set; }
        public string StatusCode { get; set; }
        public DateTime? SentDate { get; set; }
        public string Log { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual List<EmailAttach> EmailAttachs { get; set; }

		public Email()
		{
			EmailAttachs = new List<EmailAttach>();
		}
    }
}
