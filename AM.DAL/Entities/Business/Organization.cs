using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Organization : AuditableEntity
    {
        [Key]
        public int OrganizationId { get; set; }
        public int StoreId { get; set; }
        public string OrganizationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public bool Deleted { get; set; }
        public bool? CustomerAccount { get; set; }
    }
}
