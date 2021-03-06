//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.BillDetails = new HashSet<BillDetail>();
        }
    
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> AvailableForSale { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Cost { get; set; }
        public int Stock { get; set; }
        public Nullable<int> ProviderId { get; set; }
        public Nullable<int> ProductFamilyId { get; set; }
    
        public virtual Provider Provider { get; set; }
        public virtual ProductFamily ProductFamily { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
