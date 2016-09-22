using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class FormLetter : AuditableEntity
    {
        [Key]
        public int FormLetterId { get; set; }
        public int? FormTemplateId { get; set; }
        public int? ResGrpId { get; set; }
        public bool? Grp { get; set; }
        public bool IncludeLogo { get; set; }
        public DateTime? LetterDate { get; set; }
        public string CounselorLogin { get; set; }
        public string SendTypeCode { get; set; }
        public string LabelsTypeCode { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string FormLetterText { get; set; }
        public DateTime FormLetterDate { get; set; }
        public bool? Lead { get; set; }
    }
}
