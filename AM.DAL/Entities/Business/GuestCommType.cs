using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GuestCommType : AuditableEntity
    {
        [Key]
        public int GuestCommTypeId { get; set; }
        public string GuestCommTypeName { get; set; }
        public string GuestCommCategory { get; set; }
        public int? StoreId { get; set; }
        public bool? Deleted { get; set; }
        public bool? DefaultItem { get; set; }
        public string CounselorLogin { get; set; }
        public bool Ce { get; set; }
        public bool Virtuoso { get; set; }
        public string OptInMessage { get; set; }
        public string OptOutMessage { get; set; }

        public virtual List<BrandGuestCommType> BrandGuestCommType { get; set; }
        public virtual List<GuestCommPermission> GuestCommPermission { get; set; }

        public GuestCommType()
        {
            BrandGuestCommType = new List<BrandGuestCommType>();
            GuestCommPermission = new List<GuestCommPermission>();
        }
    }
}
