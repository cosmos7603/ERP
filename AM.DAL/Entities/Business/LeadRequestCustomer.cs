using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LeadRequestCustomer
    {
        [Key]
        public int LeadRequestCustomerId { get; set; }
        public int LeadRequestId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string DaytimePhone { get; set; }
        public string EveningPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool PastPassenger { get; set; }
        public string PastPassengerNumber { get; set; }
        public short LeadRequestCustomerSeq { get; set; }
    }
}
