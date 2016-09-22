using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class FormTemplate : AuditableEntity
    {
        [Key]
        public int FormTemplateId { get; set; }
        public string FormTemplateName { get; set; }
        public int StoreId { get; set; }
        public bool IncludeLogo { get; set; }
        public bool Deleted { get; set; }
        public int? CategoryCode { get; set; }
        public string FormTemplateText { get; set; }
        public int? BaseFormTemplateId { get; set; }
    }
}
