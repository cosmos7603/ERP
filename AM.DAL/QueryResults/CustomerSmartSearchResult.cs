using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class CustomerSmartSearchResult
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PriAddress1 { get; set; }
        public string PriCity { get; set; }
        public string PriStateName { get; set; }
        public string PriZip { get; set; }
        public string CounselorFullname { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public bool Deleted { get; set; }
    }
}
