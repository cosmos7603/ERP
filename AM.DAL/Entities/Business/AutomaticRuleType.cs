using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AutomaticRuleType
    {
        [Key]
        public string AutomaticRuleTypeCode { get; set; }
        public string AutomaticRuleTypeName { get; set; }
    }
}
