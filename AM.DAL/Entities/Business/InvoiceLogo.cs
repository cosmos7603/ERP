using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class InvoiceLogo
    {
        [Key]
        public int InvoiceLogoId { get; set; }
        public int StoreId { get; set; }
        public string InvoiceLogoName { get; set; }
        public int DataFileId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}
