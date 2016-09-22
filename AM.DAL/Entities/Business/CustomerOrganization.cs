using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerOrganization
    {
        [Key]
        public int CustomerOrganizationId { get; set; }
        public int CustomerId { get; set; }
        public int OrganizationId { get; set; }
    }
}
