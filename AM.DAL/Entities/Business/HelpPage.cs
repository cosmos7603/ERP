using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class HelpPage : AuditableEntity
    {
        [Key]
        public int HelpPageId { get; set; }
        public string PageUrl { get; set; }
    }
}
