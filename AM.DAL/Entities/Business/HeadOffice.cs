using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class HeadOffice
    {
        [Key]
        public string HoCode { get; set; }
        public string HoName { get; set; }
        public bool? PendingDeposit { get; set; }
        public bool? CorporateCharges { get; set; }
        public bool? DisableCashPayments { get; set; }
        public string SproutloudKey { get; set; }
        public string PopServer { get; set; }
        public int? PopServerPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }
        public int? PopTimeout { get; set; }
        public bool? PopUseSsl { get; set; }
        public bool? PopDeleteOnServer { get; set; }
        public string PromoPagePreviewUrl { get; set; }
    }
}
