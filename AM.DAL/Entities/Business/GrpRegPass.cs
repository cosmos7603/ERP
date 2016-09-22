using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpRegPass
    {
        [Key]
        public int GrpRegPassId { get; set; }
        public int GrpRegId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        public string LoyaltyPrgmNumber { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public byte PaxSeq { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public string CitizenCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public int? PhoneTypeCode { get; set; }
        public string PhoneNumber2 { get; set; }
        public int? PhoneTypeCode2 { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string NickName { get; set; }
    }
}
