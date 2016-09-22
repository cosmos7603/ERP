using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class InsurancePolicy : AuditableEntity
    {
        [Key]
        public int InsurancePolicyId { get; set; }
        public string InsurancePolicyCode { get; set; }
        public string InsurancePolicyName { get; set; }
        public int VendorId { get; set; }
        public bool Deleted { get; set; }
        public string InsurancePolicyPlanid { get; set; }
    }
}
