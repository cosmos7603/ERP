using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class BankAccount : AuditableEntity
    {
        [Key]
        public int BankAccountId { get; set; }
        public string BankAccountName { get; set; }
        public string CurrencyCode { get; set; }
        public bool DefaultItem { get; set; }
        public string CodeLevelCode { get; set; }
        public string HoCode { get; set; }
        public int? StoreId { get; set; }
        public bool Deleted { get; set; }
    }
}
