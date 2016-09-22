using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class HelpItem : AuditableEntity
    {
        [Key]
        public int HelpItemId { get; set; }
        public int HelpPageId { get; set; }
        public string HelpItemTypeCode { get; set; }
        public string HelpItemName { get; set; }
        public string HelpItemUrl { get; set; }
        public byte HelpItemSeq { get; set; }
    }
}
