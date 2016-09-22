using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class ExpenseCat : AuditableEntity
    {
        [Key]
        public int ExpenseCatId { get; set; }
        public int StoreId { get; set; }
        public string ExpenseCatName { get; set; }
        public decimal? FixedOverheadAmt { get; set; }
        public decimal? VarOverheadAmt { get; set; }
    }
}
